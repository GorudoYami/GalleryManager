using ImporterStandalone.Storage;
using ImporterStandalone.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalleryManager {
    public class Importer {
        private List<DriveInfo> drives;
        private Dictionary<DriveInfo, List<Media>> driveImports;
        private Dictionary<DriveInfo, Task> driveTask;
        private Dictionary<DriveInfo, CancellationTokenSource> driveTokenSource;

        private List<DirectoryInfo> directories;
        private Dictionary<DirectoryInfo, List<Media>> directoryImports;
        private Dictionary<DirectoryInfo, Task> directoryTask;
        private Dictionary<DirectoryInfo, CancellationTokenSource> directoryTokenSource;

        private Mutex mtx;

        private IStorage storage;
        private Indexer indexer;
        private string galleryPath;
        private Dictionary<string, Progress<int>> progress;

        private int videoCount;
        private int pictureCount;
        private int newVideos;
        private int newPictures;

        private int totalFiles;
        private int processedFiles;

        public Importer(IStorage storage, Indexer indexer, string galleryPath, Dictionary<string, Progress<int>> progress) {
            drives = new List<DriveInfo>();
            driveImports = new Dictionary<DriveInfo, List<Media>>();
            driveTask = new Dictionary<DriveInfo, Task>();
            driveTokenSource = new Dictionary<DriveInfo, CancellationTokenSource>();

            directories = new List<DirectoryInfo>();
            directoryImports = new Dictionary<DirectoryInfo, List<Media>>();
            directoryTask = new Dictionary<DirectoryInfo, Task>();
            directoryTokenSource = new Dictionary<DirectoryInfo, CancellationTokenSource>();

            mtx = new Mutex();

            this.storage = storage;
            this.indexer = indexer;
            this.galleryPath = galleryPath;
            this.progress = progress;

            videoCount = 0;
            pictureCount = 0;
            newVideos = 0;
            newPictures = 0;
            totalFiles = 0;
            processedFiles = 0;
        }

        public void AddDrive(DriveInfo drive) {
            if (drives.Contains(drive))
                throw new Exception("Drive had already been added");
            drives.Add(drive);
            driveTokenSource[drive] = new CancellationTokenSource();
            driveTask[drive] = Task.Run(() => ProcessDrive(drive, driveTokenSource[drive].Token), driveTokenSource[drive].Token);
        }

        public void AddDirectory(DirectoryInfo directory) {
            if (directories.Contains(directory))
                throw new Exception("Directory had already been added");
            directories.Add(directory);
            directoryTokenSource[directory] = new CancellationTokenSource();
            directoryTask[directory] = Task.Run(() => ProcessDirectory(directory, directoryTokenSource[directory].Token), directoryTokenSource[directory].Token);
        }

        public async Task RemoveDrive(DriveInfo drive) {
            if (!drives.Contains(drive))
                throw new Exception("Drive hadn't been added or it had been removed");
            driveTokenSource[drive].Cancel();
            await driveTask[drive];
            driveTokenSource[drive].Dispose();
            if (driveImports.ContainsKey(drive))
                driveImports.Remove(drive);
            drives.Remove(drive);
            driveTask.Remove(drive);
            driveTokenSource.Remove(drive);
        }

        public async Task RemoveDirectory(DirectoryInfo directory) {
            if (!directories.Contains(directory))
                throw new Exception("Directory hadn't been added or it had been removed");
            directoryTokenSource[directory].Cancel();
            await directoryTask[directory];
            directoryTokenSource[directory].Dispose();
            if (directoryImports.ContainsKey(directory))
                directoryImports.Remove(directory);
            directories.Remove(directory);
            directoryTask.Remove(directory);
            directoryTokenSource.Remove(directory);
        }

        public async void StopAll() {
            foreach (DriveInfo drive in drives) {
                driveTokenSource[drive].Cancel();
                await driveTask[drive];
                driveTokenSource[drive].Dispose();
            }

            foreach (DirectoryInfo directory in directories) {
                directoryTokenSource[directory].Cancel();
                await directoryTask[directory];
                directoryTokenSource[directory].Dispose();
            }
        }

        public bool IsBeingProcessed(DriveInfo drive) {
            if (drives.Contains(drive))
                return true;
            else
                return false;
        }

        public bool IsBeingProcessed(DirectoryInfo directory) {
            if (directories.Contains(directory))
                return true;
            else
                return false;
        }

        private void ProcessDrive(DriveInfo drive, CancellationToken token) {
            SHA256 sha = SHA256.Create();
            FileInfo[] files = drive.RootDirectory.GetFiles("*.*", SearchOption.AllDirectories);

            mtx.WaitOne();
            totalFiles += files.Length;
            mtx.ReleaseMutex();

            List<Media> imports = new List<Media>();

            foreach (FileInfo file in files) {
                mtx.WaitOne();
                processedFiles++;
                ((IProgress<int>)progress["overall"]).Report(processedFiles / totalFiles * 100);
                mtx.ReleaseMutex();

                if (!IsVideo(file) && !IsPicture(file) || file.Attributes.HasFlag(FileAttributes.Hidden) || file.Attributes.HasFlag(FileAttributes.System))
                    continue;
                else if (token.IsCancellationRequested)
                    return;

                mtx.WaitOne();
                if (IsVideo(file))
                    videoCount++;
                else
                    pictureCount++;
                mtx.ReleaseMutex();

                Media media = new Media() {
                    Path = file.FullName,
                    Name = file.Name,
                    Format = file.Extension,
                    Size = (ulong)file.Length
                };

                // Calculate hash
                FileStream fs = file.Open(FileMode.Open);
                byte[] hash = sha.ComputeHash(fs);
                fs.Close();
                media.Hash = BitConverter.ToString(hash).Replace("-", string.Empty);

                // Check if media exists in gallery
                if (!storage.Contains(media)) {
                    imports.Add(media);
                    mtx.WaitOne();
                    if (IsVideo(file))
                        newVideos++;
                    else
                        newPictures++;
                    mtx.ReleaseMutex();
                }
                ((IProgress<int>)progress["newVideos"]).Report(newVideos);
                ((IProgress<int>)progress["newPictures"]).Report(newPictures);
                ((IProgress<int>)progress["videoCount"]).Report(videoCount);
                ((IProgress<int>)progress["pictureCount"]).Report(pictureCount);
            }

            mtx.WaitOne();
            driveImports[drive] = imports;
            mtx.ReleaseMutex();
        }

        private void ProcessDirectory(DirectoryInfo directory, CancellationToken token) {
            SHA256 sha = SHA256.Create();
            FileInfo[] files = directory.GetFiles("*.*", SearchOption.AllDirectories);

            mtx.WaitOne();
            totalFiles += files.Length;
            mtx.ReleaseMutex();

            List<Media> imports = new List<Media>();

            foreach (FileInfo file in files) {
                mtx.WaitOne();
                processedFiles++;
                int pVal = (int)Math.Round((double)processedFiles / totalFiles * 1000.0);
                ((IProgress<int>)progress["overall"]).Report(pVal);
                mtx.ReleaseMutex();

                if (!IsVideo(file) && !IsPicture(file) || file.Attributes.HasFlag(FileAttributes.Hidden) || file.Attributes.HasFlag(FileAttributes.System))
                    continue;
                else if (token.IsCancellationRequested)
                    return;

                if (IsVideo(file))
                    videoCount++;
                else
                    pictureCount++;

                Media media = new Media() {
                    Path = file.FullName,
                    Name = file.Name,
                    Format = file.Extension,
                    Size = (ulong)file.Length
                };


                // Calculate hash
                FileStream fs = file.Open(FileMode.Open);
                byte[] hash = sha.ComputeHash(fs);
                fs.Close();
                media.Hash = BitConverter.ToString(hash).Replace("-", string.Empty);

                // Check if media exists in gallery
                if (!storage.Contains(media)) {
                    imports.Add(media);
                    if (IsVideo(file))
                        newVideos++;
                    else
                        newPictures++;
                }
                ((IProgress<int>)progress["newVideos"]).Report(newVideos);
                ((IProgress<int>)progress["newPictures"]).Report(newPictures);
                ((IProgress<int>)progress["videoCount"]).Report(videoCount);
                ((IProgress<int>)progress["pictureCount"]).Report(pictureCount);
            }

            mtx.WaitOne();
            directoryImports[directory] = imports;
            mtx.ReleaseMutex();
        }

        public async void Import() {
            await Task.Run(() => {

                // Calculate total imports
                int totalFiles = 0;
                foreach (DriveInfo drive in drives) {
                    List<Media> mediaList = driveImports[drive];
                    totalFiles += mediaList.Count;
                }

                foreach (DirectoryInfo directory in directories) {
                    List<Media> mediaList = directoryImports[directory];
                    totalFiles += mediaList.Count;
                }

                // Iterate drives
                int importedFiles = 0;
                foreach (DriveInfo drive in drives) {
                    List<Media> mediaList = driveImports[drive];
                    if (mediaList.Count == 0)
                        continue;

                    // Iterate media for import
                    foreach (Media media in mediaList) {
                        // Create a subdir in gallery if there isn't one matching the pattern:
                        // yyyy-MM
                        string dirname = File.GetCreationTime(media.Path).ToString("yyyy-MM");
                        DirectoryInfo subdir = new DirectoryInfo(galleryPath + "\\" + dirname);
                        if (!subdir.Exists)
                            subdir.Create();

                        // Make sure that the file has a unique name
                        string name = media.Name;
                        int n = 1;
                        while (File.Exists(subdir.FullName + "\\" + name))
                            name = media.Name + "_" + n + media.Format;
                        File.Copy(media.Path, subdir.FullName + "\\" + name);
                        media.Path = subdir.FullName + "\\" + name;
                        indexer.IndexMedia(media);
                        importedFiles++;
                        ((IProgress<int>)progress["overall"]).Report(importedFiles / totalFiles * 100);
                    }
                }

                // Iterate directories
                foreach (DirectoryInfo directory in directories) {
                    List<Media> mediaList = directoryImports[directory];
                    if (mediaList.Count == 0)
                        continue;

                    // Iterate media for import
                    foreach (Media media in mediaList) {
                        // Create a subdir in gallery if there isn't one matching the pattern:
                        // yyyy-MM
                        string dirname = File.GetCreationTime(media.Path).ToString("yyyy-MM");
                        DirectoryInfo subdir = new DirectoryInfo(galleryPath + "\\" + dirname);
                        if (!subdir.Exists)
                            subdir.Create();

                        // Make sure that the file has a unique name
                        string name = media.Name;
                        int n = 1;
                        while (File.Exists(subdir.FullName + "\\" + name))
                            name = media.Name + "_" + n + media.Format;
                        File.Copy(media.Path, subdir.FullName + "\\" + name);
                        media.Path = subdir.FullName + "\\" + name;
                        indexer.IndexMedia(media);
                        importedFiles++;
                        ((IProgress<int>)progress["overall"]).Report(importedFiles / totalFiles * 100);
                    }
                }
            });
        }

        public Task<bool> ImportReadyAsync() {
            return Task.Run(() => {
                bool result = true;
                mtx.WaitOne();
                foreach (DriveInfo drive in drives) {
                    if (!driveImports.ContainsKey(drive)) {
                        result = false;
                        break;
                    }
                }

                foreach (DirectoryInfo directory in directories) {
                    if (!directoryImports.ContainsKey(directory)) {
                        result = false;
                        break;
                    }
                }
                mtx.ReleaseMutex();
                return result;
            });
        }

        private bool IsVideo(FileInfo file) {
            switch (file.Extension.ToUpper()) {
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

        private bool IsPicture(FileInfo file) {
            switch (file.Extension.ToUpper()) {
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
                case ".NEF":
                case ".CGM":
                    return true;
                default:
                    return false;
            }
        }
    }
}
