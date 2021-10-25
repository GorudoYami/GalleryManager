using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

using GalleryManager.Storage;
using GalleryManager.Types;

namespace GalleryManager {
    public class Importer {
        // IMPORTANT
        // TODO
        // Merge DriveInfo and DirectoryInfo?
        // It's derived from FileSystemInfo

        private readonly List<DriveInfo> drives;
        private readonly Dictionary<DriveInfo, List<Media>> driveImports;
        private readonly Dictionary<DriveInfo, Task> driveTask;
        private readonly Dictionary<DriveInfo, CancellationTokenSource> driveTokenSource;
        private readonly Dictionary<DriveInfo, Dictionary<string, int>> driveFileCount;

        private readonly List<DirectoryInfo> directories;
        private readonly Dictionary<DirectoryInfo, List<Media>> directoryImports;
        private readonly Dictionary<DirectoryInfo, Task> directoryTask;
        private readonly Dictionary<DirectoryInfo, CancellationTokenSource> directoryTokenSource;
        private readonly Dictionary<DirectoryInfo, Dictionary<string, int>> directoryFileCount;

        // Mutex for sync
        private readonly Mutex mtx;

        private readonly IStorage storage;
        private readonly string galleryPath;
 
        public Importer(IStorage storage, string galleryPath) {
            drives = new List<DriveInfo>();
            driveImports = new Dictionary<DriveInfo, List<Media>>();
            driveTask = new Dictionary<DriveInfo, Task>();
            driveTokenSource = new Dictionary<DriveInfo, CancellationTokenSource>();
            driveFileCount = new Dictionary<DriveInfo, Dictionary<string, int>>();

            directories = new List<DirectoryInfo>();
            directoryImports = new Dictionary<DirectoryInfo, List<Media>>();
            directoryTask = new Dictionary<DirectoryInfo, Task>();
            directoryTokenSource = new Dictionary<DirectoryInfo, CancellationTokenSource>();
            directoryFileCount = new Dictionary<DirectoryInfo, Dictionary<string, int>>();

            mtx = new Mutex();

            this.storage = storage;
            this.galleryPath = galleryPath.Replace("\\", "/");
        }

        public void AddDrive(DriveInfo drive) {
            Dictionary<string, int> fileCount = new();
            fileCount["total"] = 0;
            fileCount["processed"] = 0;
            fileCount["videos"] = 0;
            fileCount["pictures"] = 0;
            fileCount["new-videos"] = 0;
            fileCount["new-pictures"] = 0;

            mtx.WaitOne();
            if (drives.Contains(drive))
                throw new Exception("Drive has already been added");
            drives.Add(drive);
            driveFileCount[drive] = fileCount;
            driveTokenSource[drive] = new CancellationTokenSource();
            driveTask[drive] = Task.Run(() => ProcessDrive(drive, driveTokenSource[drive].Token), driveTokenSource[drive].Token);
            mtx.ReleaseMutex();
        }

        public void AddDirectory(DirectoryInfo directory) {
            Dictionary<string, int> fileCount = new();
            fileCount["total"] = 0;
            fileCount["processed"] = 0;
            fileCount["videos"] = 0;
            fileCount["pictures"] = 0;
            fileCount["new-videos"] = 0;
            fileCount["new-pictures"] = 0;

            mtx.WaitOne();
            if (directories.Contains(directory)) {
                Debug.WriteLine("Directory has been already added!");
                mtx.ReleaseMutex();
                return;
            }
            directories.Add(directory);
            directoryFileCount[directory] = fileCount;
            directoryTokenSource[directory] = new CancellationTokenSource();
            directoryTask[directory] = Task.Run(() => ProcessDirectory(directory, directoryTokenSource[directory].Token), directoryTokenSource[directory].Token);
            mtx.ReleaseMutex();
        }

        public async Task RemoveDriveAsync(DriveInfo drive) {
            mtx.WaitOne();
            if (!drives.Contains(drive)) {
                Debug.WriteLine("Drive is not being processed!");
                mtx.ReleaseMutex();
                return;
            }

            driveTokenSource[drive].Cancel();
            await driveTask[drive];
            driveTokenSource[drive].Dispose();

            if (driveImports.ContainsKey(drive))
                driveImports.Remove(drive);
            drives.Remove(drive);
            driveTask.Remove(drive);
            driveTokenSource.Remove(drive);
            driveFileCount.Remove(drive);
            mtx.ReleaseMutex();
        }

        public async Task RemoveDirectoryAsync(DirectoryInfo directory) {
            mtx.WaitOne();
            if (!directories.Contains(directory)) {
                Debug.WriteLine("Directory is not being processed!");
                mtx.ReleaseMutex();
                return;
            }

            directoryTokenSource[directory].Cancel();
            await directoryTask[directory];
            directoryTokenSource[directory].Dispose();

            if (directoryImports.ContainsKey(directory))
                directoryImports.Remove(directory);
            directories.Remove(directory);
            directoryTask.Remove(directory);
            directoryTokenSource.Remove(directory);
            directoryFileCount.Remove(directory);
            mtx.ReleaseMutex();
        }

        public async void RemoveAll() {
            mtx.WaitOne();
            
            // Iterate through processed drives
            // Stop them and then cleanup
            foreach (DriveInfo drive in drives) {
                driveTokenSource[drive].Cancel();
                await driveTask[drive];
                driveTokenSource[drive].Dispose();
            }

            drives.Clear();
            driveImports.Clear();
            driveTask.Clear();
            driveTokenSource.Clear();
            driveFileCount.Clear();

            // Iterate through processed drives
            // Stop them and then cleanup
            foreach (DirectoryInfo directory in directories) {
                directoryTokenSource[directory].Cancel();
                await directoryTask[directory];
                directoryTokenSource[directory].Dispose();
            }
            directories.Clear();
            directoryImports.Clear();
            directoryTask.Clear();
            directoryTokenSource.Clear();
            directoryFileCount.Clear();

            mtx.ReleaseMutex();
        }

        public bool IsAdded(DriveInfo drive) =>
            drives.Contains(drive);

        public bool IsAdded(DirectoryInfo directory) =>
            directories.Contains(directory);

        private void ProcessDrive(DriveInfo drive, CancellationToken token) {
            SHA256 sha = SHA256.Create();
            FileInfo[] files = drive.RootDirectory.GetFiles("*.*", SearchOption.AllDirectories);

            // Get dictionary for import stats
            mtx.WaitOne();
            Dictionary<string, int> fileCount = driveFileCount[drive];
            mtx.ReleaseMutex();
            fileCount["total"] = files.Length;

            List<Media> imports = new();

            foreach (FileInfo file in files) {
                fileCount["processed"]++;

                if (!Media.IsVideo(file) && !Media.IsPicture(file) || file.Attributes.HasFlag(FileAttributes.Hidden) || file.Attributes.HasFlag(FileAttributes.System))
                    continue;
                else if (token.IsCancellationRequested)
                    return;

                if (Media.IsVideo(file))
                    fileCount["videos"]++;
                else
                    fileCount["pictures"]++;

                Media media = new() {
                    Path = file.FullName.Replace("\\", "/"),
                    Name = file.Name,
                    Format = file.Extension,
                    Size = (ulong)file.Length
                };

                // Calculate hash
                try {
                    FileStream fs = file.Open(FileMode.Open);
                    byte[] hash = sha.ComputeHash(fs);
                    fs.Close();
                    media.Hash = BitConverter.ToString(hash).Replace("-", string.Empty);
                }
                catch (Exception e) {
                    Debug.WriteLine(e.GetType());
                    Debug.WriteLine(e.Message);
                    continue;
                }

            // Check if media exists in gallery
            if (!storage.Contains(media)) {
                    imports.Add(media);
                    if (Media.IsVideo(file))
                        fileCount["new-videos"]++;
                    else
                        fileCount["new-pictures"]++;
                }

                mtx.WaitOne();
                driveFileCount[drive] = fileCount;
                mtx.ReleaseMutex();
            }

            mtx.WaitOne();
            driveImports[drive] = imports;
            mtx.ReleaseMutex();
            Debug.WriteLine("Drive '" + drive.Name + "' has been processed:");
            Debug.WriteLine("Total files: " + fileCount["total"]);
            Debug.WriteLine("Total pictures: " + fileCount["pictures"]);
            Debug.WriteLine("Total videos: " + fileCount["videos"]);
            Debug.WriteLine("New pictures: " + fileCount["new-pictures"]);
            Debug.WriteLine("New videos: " + fileCount["new-videos"]);
        }

        private void ProcessDirectory(DirectoryInfo directory, CancellationToken token) {
            SHA256 sha = SHA256.Create();
            FileInfo[] files = directory.GetFiles("*.*", SearchOption.AllDirectories);

            mtx.WaitOne();
            Dictionary<string, int> fileCount = directoryFileCount[directory];
            mtx.ReleaseMutex();
            fileCount["total"] = files.Length;

            List<Media> imports = new();

            foreach (FileInfo file in files) {
                fileCount["processed"]++;

                if (!Media.IsVideo(file) && !Media.IsPicture(file) || file.Attributes.HasFlag(FileAttributes.Hidden) || file.Attributes.HasFlag(FileAttributes.System))
                    continue;
                else if (token.IsCancellationRequested)
                    return;

                if (Media.IsVideo(file))
                    fileCount["videos"]++;
                else
                   fileCount["pictures"]++;

                Media media = new() {
                    Path = file.FullName.Replace("\\", "/"),
                    Name = file.Name,
                    Format = file.Extension,
                    Size = (ulong)file.Length
                };


                // Calculate hash
                try {
                    FileStream fs = file.Open(FileMode.Open);
                    byte[] hash = sha.ComputeHash(fs);
                    fs.Close();
                    media.Hash = BitConverter.ToString(hash).Replace("-", string.Empty);
                }
                catch (Exception e) {
                    Debug.WriteLine(e.GetType());
                    Debug.WriteLine(e.Message);
                    continue;
                }

                // Check if media exists in gallery
                if (!storage.Contains(media)) {
                    imports.Add(media);
                    if (Media.IsVideo(file))
                        fileCount["new-videos"]++;
                    else
                        fileCount["new-pictures"]++;
                }

                mtx.WaitOne();
                directoryFileCount[directory] = fileCount;
                mtx.ReleaseMutex();
            }

            mtx.WaitOne();
            directoryImports[directory] = imports;
            mtx.ReleaseMutex();
            Debug.WriteLine("Directory '" + directory.Name + "' has been processed:");
            Debug.WriteLine("Total files: " + fileCount["total"]);
            Debug.WriteLine("Total pictures: " + fileCount["pictures"]);
            Debug.WriteLine("Total videos: " + fileCount["videos"]);
            Debug.WriteLine("New pictures: " + fileCount["new-pictures"]);
            Debug.WriteLine("New videos: " + fileCount["new-videos"]);
        }

        public async void StartImport() {
            await Task.Run(() => {
                mtx.WaitOne();

                int importedFiles = 0;
                int totalFiles = 0;
                // Calculate total imports
                foreach (DriveInfo drive in drives) {
                    List<Media> mediaList = driveImports[drive];
                    totalFiles += mediaList.Count;
                }

                foreach (DirectoryInfo directory in directories) {
                    List<Media> mediaList = directoryImports[directory];
                    totalFiles += mediaList.Count;
                }

                // Iterate drives
                foreach (DriveInfo drive in drives) {
                    List<Media> mediaList = driveImports[drive];
                    if (mediaList.Count == 0)
                        continue;
                    importedFiles += Import(mediaList);
                }

                // Iterate directories
                foreach (DirectoryInfo directory in directories) {
                    List<Media> mediaList = directoryImports[directory];
                    if (mediaList.Count == 0)
                        continue;
                    importedFiles += Import(mediaList);
                }

                mtx.ReleaseMutex();
                Debug.WriteLine("Importing process has been completed!");
                Debug.WriteLine("Total imported files: " + importedFiles);
            });
        }

        private int Import(List<Media> mediaList) {
            int importedFiles = 0;
            foreach (Media media in mediaList) {
                // Create a subdir in gallery if there isn't one matching the pattern:
                // yyyy-MM
                string dirName = File.GetLastWriteTime(media.Path).ToString("yyyy-MM");
                DirectoryInfo subdir = new(galleryPath + "/" + dirName);
                try {
                    if (!Directory.Exists(subdir.FullName))
                        subdir.Create();
                }
                catch (Exception e) {
                    Debug.WriteLine("ERROR: " + e.GetType());
                    Debug.WriteLine(e.Message);
                    continue;
                }

                // Make sure that the file has a unique name
                string oldName = media.Name.Remove(media.Name.LastIndexOf("."));
                string newName = oldName;
                int n = 1;
                while (File.Exists(subdir.FullName + "/" + newName + media.Format))
                    newName = oldName + " (" + n++ + ")";

                // Copy
                try {
                    File.Copy(media.Path, subdir.FullName + "/" + newName + media.Format);
                }
                catch (Exception e) {
                    Debug.WriteLine("ERROR: " + e.GetType());
                    Debug.WriteLine(e.Message);
                    continue;
                }

                // Change path after copying
                media.Path = subdir.FullName.Replace("\\", "/") + "/" + newName + media.Format;
                media.Name = newName;
                media.RelativePath = media.Path.Remove(0, galleryPath.Length + 1).Replace("\\", "/");

                // Insert index record into db
                if (Media.IsPicture(media))
                    storage.AddPicture(media);
                else if (Media.IsVideo(media))
                    storage.AddVideo(media);

                importedFiles++;
            }
            return importedFiles;
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
    }
}
