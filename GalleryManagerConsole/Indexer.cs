using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

using GalleryManagerConsole.Storage;
using GalleryManagerConsole.Types;
using GalleryManagerConsole.ConsoleMenu;

namespace GalleryManagerConsole {
    public class Indexer {

        private bool working;
        public bool Working { get { return working; } }

        private ulong filesCount;
        public ulong FileCount { get { return filesCount; } }

        private ulong filesIndexed;
        public ulong FilesIndexed { get { return filesIndexed; } }

        private readonly IStorage storage;
        private Task task;
        private CancellationTokenSource tokenSource;
        private readonly string galleryPath;
        private readonly Menu menu;

        public Indexer(IStorage storage, string galleryPath, Menu menu) {
            this.storage = storage;
            this.galleryPath = galleryPath.Replace("\\", "/");
            this.menu = menu;
            filesCount = 0;
            filesIndexed = 0;
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
                menu.ShowMessage(e.GetType().ToString());
                menu.ShowMessage(e.Message);
                working = false;
                return;
            }
            filesCount = (ulong)files.Length;

            // Iterate through all files in gallery
            for (int i = 0; i < files.Length; i++) {
                if (token.IsCancellationRequested)
                    break;

                filesIndexed++;

                Media media = new() {
                    Path = files[i].FullName.Replace("\\", "/"),
                    RelativePath = files[i].FullName.Remove(0, galleryPath.Length + 1).Replace("\\", "/"),
                    Name = files[i].Name,
                    Format = files[i].Extension,
                    Size = (ulong)files[i].Length
                };

                if (storage.IsIndexed(media))
                    continue;

                if (!Media.IsPicture(media) && !Media.IsVideo(media)) {
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
                    menu.ShowMessage(e.GetType().ToString());
                    menu.ShowMessage(e.Message);
                    working = false;
                    return;
                }
                media.Hash = BitConverter.ToString(hash).Replace("-", string.Empty);

                if (Media.IsPicture(media))
                    storage.AddPicture(media);
                else if (Media.IsVideo(media))
                    storage.AddVideo(media);
            }
            menu.ShowMessage("Indexer finished work:");
            menu.ShowMessage("Files indexed: " + FilesIndexed);
            working = false;
        }
    }
}
