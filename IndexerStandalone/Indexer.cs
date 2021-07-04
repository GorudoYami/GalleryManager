using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace IndexerStandalone {
    internal class Indexer {

        private bool working;
        public bool Working { get { return working; } }

        private ulong filesCount;
        public ulong FilesCount { get { return filesCount; } }

        private readonly IProgress<double> progress;

        private readonly MySqlStorage storage;
        private Task task;
        private CancellationTokenSource tokenSource;
        private readonly string galleryPath;

        public Indexer(MySqlStorage storage, string galleryPath, IProgress<double> progress) {
            this.storage = storage;
            this.galleryPath = galleryPath;
            this.progress = progress;
            filesCount = 0;
        }

        public void Start() {
            tokenSource = new CancellationTokenSource();
            task = Task.Run(() => Run(tokenSource.Token), tokenSource.Token);
            working = true;
        }

        public async void Stop() {
            tokenSource.Cancel();
            await task;
            working = false;
            tokenSource.Dispose();
        }

        private void Run(CancellationToken token) {
            DirectoryInfo root;
            FileInfo[] files;
            using SHA256 sha = SHA256.Create();

            // Get all files
            try {
                root = new DirectoryInfo(galleryPath);
                files = root.GetFiles("*.*", SearchOption.AllDirectories);
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                working = false;
                return;
            }
            filesCount = (ulong)files.Length;

            // Iterate through all files in gallery
            for (int i = 0; i < files.Length; i++) {
                if (token.IsCancellationRequested)
                    break;

                progress.Report(Math.Round((double)i / filesCount * 1000.0) / 10.0);

                Media media = new() {
                    Path = files[i].FullName,
                    RelativePath = files[i].FullName.Remove(0, galleryPath.Length + 1),
                    Name = files[i].Name,
                    Format = files[i].Extension,
                    Size = (ulong)files[i].Length
                };

                if (storage.IsIndexed(media))
                    continue;

                if (!IsPicture(media) && !IsVideo(media)) {
                    storage.AddUnknown(media);
                    continue;
                }

                // Calculate hash
                byte[] hash;
                try {
                    FileStream file = files[i].Open(FileMode.Open);
                    hash = sha.ComputeHash(file);
                    file.Close();
                }
                catch (Exception e) {
                    Console.WriteLine(e.GetType());
                    Console.WriteLine(e.Message);
                    working = false;
                    return;
                }
                media.Hash = BitConverter.ToString(hash).Replace("-", string.Empty);

                if (IsPicture(media))
                    storage.AddPicture(media);
                else if (IsVideo(media))
                    storage.AddVideo(media);
            }
            working = false;
        }

        private static bool IsVideo(Media media) {
            string ext = media.Format.ToUpper();
            switch (ext) {
                case ".MP4":
                case ".WMV":
                case ".MOV":
                case ".FLV":
                case ".AVI":
                case ".WEBM":
                case ".AVCHD":
                case ".MKV":
                case ".OGG":
                case ".VOB":
                case ".QT":
                case ".YUV":
                case ".AMV":
                case ".M4P":
                case ".M4V":
                case ".MPG":
                case ".MP2":
                case ".MPEG":
                case ".MPE":
                case ".MPV":
                case ".3GP":
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsPicture(Media media) {
            string ext = media.Format.ToUpper();
            switch (ext) {
                case ".TIF":
                case ".TIFF":
                case ".BMP":
                case ".JPG":
                case ".JPEG":
                case ".GIF":
                case ".PNG":
                case ".EPS":
                case ".RAW":
                case ".WEBP":
                case ".IMG":
                case ".SVG":
                case ".CGM":
                    return true;
                default:
                    return false;
            }
        }
    }
}
