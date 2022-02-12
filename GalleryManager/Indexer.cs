using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Cryptography;

using GalleryManager.Storage;
using GalleryManager.Models;
using GalleryManager.Common;

namespace GalleryManager;

public class Indexer {
	public bool Working { get; private set; }
	public int FileCount { get; private set; }
	public int IndexedCount { get; private set; }
	public string GalleryPath { get; set; }

	private readonly IIndexStorage Storage;
	private Task MainTask;
	private CancellationTokenSource TokenSource;

	public Indexer(IIndexStorage storage, string galleryPath = null) {
		if (galleryPath is not null)
			GalleryPath = galleryPath.Replace("\\", "/");

		Storage = storage;
		FileCount = 0;
		IndexedCount = 0;
	}

	public void Start() {
		TokenSource = new CancellationTokenSource();
		MainTask = Task.Run(() => Run(TokenSource.Token), TokenSource.Token);
		Working = true;
	}

	public async void Stop() {
		TokenSource.Cancel();
		await MainTask;
		Working = false;
		TokenSource.Dispose();
	}

	private void Run(CancellationToken token) {
		DirectoryInfo root;
		FileInfo[] files;

		try {
			root = new DirectoryInfo(GalleryPath);
			files = root.GetFiles("*.*", SearchOption.AllDirectories);
		}
		catch (Exception e) {
			Debug.WriteLine(e.GetType().ToString());
			Debug.WriteLine(e.Message);
			Working = false;
			return;
		}
		FileCount = files.Length;

		foreach (FileInfo fileInfo in files) {
			if (token.IsCancellationRequested)
				break;

			IndexedCount++;

			Media media = new(fileInfo, GalleryPath);

			if (Storage.IsIndexed(media))
				continue;

			if (!Media.IsPicture(media) && !Media.IsVideo(media)) {
				Storage.AddUnknown(media);
				continue;
			}
			media.Hash = FileUtils.GetFileHash(fileInfo);

			if (Media.IsPicture(media))
				Storage.AddPicture(media);
			else if (Media.IsVideo(media))
				Storage.AddVideo(media);
		}

		Debug.WriteLine("Indexer finished work");
		Debug.WriteLine($"Files indexed: {IndexedCount}");
		Working = false;
	}
}
