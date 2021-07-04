using GalleryManager.Storage;
using GalleryManager.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalleryManager {
    public class Indexer {
        public enum Mode {
            MYSQL,
            SQLITE,
            JSON
        }

        private Mode currentMode;
        public Mode CurrentMode { get { return currentMode; } }

        private bool working;
        public bool Working { get { return working; } }

        private ulong filesCount;
        public ulong FilesCount { get { return filesCount; } }

        private IProgress<double> progress;

        private IStorage storage;
        private Task task;
        private CancellationTokenSource tokenSource;
        private string galleryPath;

        public Indexer(Mode mode, IStorage storage, string galleryPath, IProgress<double> progress) {
            currentMode = mode;
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

        public async Task Stop() {
            tokenSource.Cancel();
            await task;
            working = false;
            tokenSource.Dispose();
        }

        public async Task UpdateGalleryPath(string galleryPath) {
            bool wasWorking = false;
            if (working) {
                await Stop();
                wasWorking = true;
            }
            this.galleryPath = galleryPath;
            if (wasWorking)
                Start();
        }

        public void IndexMedia(Media media) {
            using SHA256 sha = SHA256.Create();

            // Calculate hash
            byte[] hash;
            try {
                FileStream file = File.OpenRead(media.Path);
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

            // Add to database
            if (!storage.IsIndexed(media)) {
                if (IsVideo(media))
                    storage.AddVideo(media);
                else if (IsPicture(media))
                    storage.AddPicture(media);
            }
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

            if (CurrentMode == Mode.MYSQL) {
                for (int i = 0; i < files.Length; i++) {
                    progress.Report(Math.Round((double)i / filesCount * 1000.0) / 10.0);
                    if (token.IsCancellationRequested)
                        break;

                    Media media = new Media {
                        Path = files[i].FullName,
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
            }
            else if (CurrentMode == Mode.SQLITE) {

            }
            else if (CurrentMode == Mode.JSON) {

            }
            working = false;
        }

        private bool IsVideo(Media media) {
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

        private bool IsPicture(Media media) {
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
