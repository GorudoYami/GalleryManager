using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GalleryManager.Common;

public static class FileUtils {

	public static string GetFileHash(FileInfo fileInfo) {
		string hashStr = string.Empty;

		try {
			using FileStream file = fileInfo.Open(FileMode.Open);
			using SHA256 sha = SHA256.Create();
			byte[] hash = sha.ComputeHash(file);
			file.Close();

			hashStr = BitConverter.ToString(hash).Replace("-", string.Empty);
		}
		catch (Exception e) {
			Debug.WriteLine(e.GetType().ToString());
			Debug.WriteLine(e.Message);
		}

		return hashStr;
	}

	public static string NormalizeDirectoryPath(string path) {
		string result = NormalizePath(path);

		if (!result.EndsWith('/'))
			result += '/';

		return result;
	}

	public static string NormalizePath(string path) =>
		path.Replace("\\", "/");

	public static FileInfo NewFileNameIfExists(FileInfo file) {
		if (!file.Exists)
			return file;

		string baseFileName = file.Name.Remove(file.Name.LastIndexOf('.'));
		string directoryName = NormalizeDirectoryPath(file.DirectoryName);
		string extension = file.Extension;

		int n = 1;
		while (file.Exists)
			file = new FileInfo($"{directoryName}{baseFileName} ({n}){extension}");

		return file;
	}
}
