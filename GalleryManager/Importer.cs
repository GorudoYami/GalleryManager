using GalleryManager.Storage;
using GalleryManager.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GalleryManager.Types;
using GalleryManager.Common;
using System.Collections.Concurrent;

namespace GalleryManager;

public class Importer {
	private readonly ConcurrentDictionary<DriveInfo, ImporterInfo> Drives;
	private readonly ConcurrentDictionary<DirectoryInfo, ImporterInfo> Directories;
	private readonly IIndexStorage Storage;
	private readonly string GalleryPath;

	public Importer(IIndexStorage storage, string galleryPath) {
		Drives = new ConcurrentDictionary<DriveInfo, ImporterInfo>();
		Directories = new ConcurrentDictionary<DirectoryInfo, ImporterInfo>();
		GalleryPath = FileUtils.NormalizeDirectoryPath(galleryPath);
		Storage = storage;
	}

	public void AddDrive(DriveInfo drive) {
		if (Drives.ContainsKey(drive)) {
			Debug.WriteLine("Drive has already been added");
			return;
		}

		Drives[drive] = new ImporterInfo();
		CancellationTokenSource tokenSource = Drives[drive].TokenSource;
		Drives[drive].Task = Task.Run(() => ProcessDrive(drive), tokenSource.Token);
	}

	public void AddDirectory(DirectoryInfo directory) {
		if (Directories.ContainsKey(directory)) {
			Debug.WriteLine("Directory has been already added!");
			return;
		}

		Directories[directory] = new ImporterInfo();
		CancellationTokenSource tokenSource = Directories[directory].TokenSource;
		Directories[directory].Task = Task.Run(() => ProcessDirectory(directory), tokenSource.Token);
	}

	public async Task RemoveDriveAsync(DriveInfo drive) {
		if (!Drives.ContainsKey(drive)) {
			Debug.WriteLine("Drive is not being processed!");
			return;
		}

		Drives[drive].TokenSource.Cancel();
		await Drives[drive].Task;
		Drives[drive].TokenSource.Dispose();

		await Task.Run(() => {
			while (!Drives.TryRemove(drive, out _)) ;
		});
	}

	public async Task RemoveDirectoryAsync(DirectoryInfo directory) {
		if (!Directories.ContainsKey(directory)) {
			Debug.WriteLine("Directory is not being processed!");
			return;
		}

		Directories[directory].TokenSource.Cancel();
		await Directories[directory].Task;
		Directories[directory].TokenSource.Dispose();

		await Task.Run(() => {
			while (!Directories.TryRemove(directory, out _)) ;
		});
	}

	public async void RemoveAllAsync() {
		foreach (DriveInfo drive in Drives.Keys) {
			Drives[drive].TokenSource.Cancel();
			await Drives[drive].Task;
			Drives[drive].TokenSource.Dispose();
		}
		Drives.Clear();

		foreach (DirectoryInfo directory in Directories.Keys) {
			Directories[directory].TokenSource.Cancel();
			await Directories[directory].Task;
			Directories[directory].TokenSource.Dispose();
		}
		Directories.Clear();
	}

	public bool IsAdded(DriveInfo drive) =>
		Drives.ContainsKey(drive);

	public bool IsAdded(DirectoryInfo directory) =>
		Directories.ContainsKey(directory);

	private void ProcessDrive(DriveInfo drive) {
		FileInfo[] files = drive.RootDirectory.GetFiles("*.*", SearchOption.AllDirectories);

		Drives[drive].FileCount.Total = files.Length;
		ProcessFiles(files, Drives[drive]);

		Debug.WriteLine($"Drive \"{drive.Name}\" has been processed");
		WriteStatsToDebug(Drives[drive].FileCount);
	}

	private void ProcessDirectory(DirectoryInfo directory) {
		FileInfo[] files = directory.GetFiles("*.*", SearchOption.AllDirectories);

		Directories[directory].FileCount.Total = files.Length;
		ProcessFiles(files, Directories[directory]);

		Debug.WriteLine($"Directory \"{directory.Name}\" has been processed");
		WriteStatsToDebug(Directories[directory].FileCount);
	}

	private static void WriteStatsToDebug(FileCount fileCount) {
		Debug.WriteLine($"Total files: {fileCount.Total}");
		Debug.WriteLine($"Total pictures: {fileCount.Pictures}");
		Debug.WriteLine($"Total videos: {fileCount.Videos}");
		Debug.WriteLine($"New pictures: {fileCount.NewPictures}");
		Debug.WriteLine($"New videos: {fileCount.NewVideos}");
	}

	private void ProcessFiles(FileInfo[] files, ImporterInfo importerInfo) {
		CancellationToken token = importerInfo.TokenSource.Token;

		foreach (FileInfo fileInfo in files) {
			importerInfo.FileCount.Processed++;

			// Skip non-media and system files
			if (!Media.IsVideo(fileInfo) && !Media.IsPicture(fileInfo) || fileInfo.Attributes.HasFlag(FileAttributes.Hidden) || fileInfo.Attributes.HasFlag(FileAttributes.System))
				continue;
			else if (token.IsCancellationRequested)
				return;

			if (Media.IsVideo(fileInfo))
				importerInfo.FileCount.Videos++;
			else
				importerInfo.FileCount.Pictures++;

			Media media = new(fileInfo, GalleryPath);
			media.Hash = FileUtils.GetFileHash(fileInfo);

			// Check if media exists in gallery
			if (!Storage.Contains(media)) {
				importerInfo.Imports.Add(media);
				if (Media.IsVideo(fileInfo))
					importerInfo.FileCount.NewVideos++;
				else
					importerInfo.FileCount.NewVideos++;
			}
		}
	}

	public async void StartImport() {
		await Task.Run(() => {
			int importedFiles = 0;

			foreach (DriveInfo driveInfo in Drives.Keys)
				importedFiles += Import(Drives[driveInfo].Imports);

			foreach (DirectoryInfo directoryInfo in Directories.Keys)
				importedFiles += Import(Directories[directoryInfo].Imports);

			Debug.WriteLine("Importing process has been completed!");
			Debug.WriteLine("Total imported files: " + importedFiles);
		});
	}

	private int Import(List<Media> imports) {
		int importedFiles = 0;

		foreach (Media media in imports) {
			// Create a subdir in gallery if there isn't one matching the pattern:
			// yyyy-MM
			string directoryName = File.GetLastWriteTime(media.Path).ToString("yyyy-MM");
			DirectoryInfo subDirectory = new(FileUtils.NormalizeDirectoryPath(GalleryPath + directoryName));

			if (!Directory.Exists(subDirectory.FullName))
				subDirectory.Create();

			FileInfo file = FileUtils.NewFileNameIfExists(new FileInfo(subDirectory.FullName + media.Name));

			File.Copy(media.Path, file.FullName);

			// Change path after copying
			media.Path = file.FullName;
			media.Name = file.Name;
			media.RelativePath = media.Path.Remove(0, GalleryPath.Length + 1).Replace("\\", "/");

			// Insert index record into db
			if (Media.IsPicture(media))
				Storage.AddPicture(media);
			else if (Media.IsVideo(media))
				Storage.AddVideo(media);

			importedFiles++;
		}

		return importedFiles;
	}

	public bool ImportReady() {
		foreach (DriveInfo driveInfo in Drives.Keys) {
			if (!Drives[driveInfo].Task.IsCompleted)
				return false;
		}

		foreach (DirectoryInfo directoryInfo in Directories.Keys) {
			if (!Directories[directoryInfo].Task.IsCompleted)
				return false;
		}

		return true;
	}
}
