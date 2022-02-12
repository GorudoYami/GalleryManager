using System.IO;

namespace GalleryManager.Models;

public enum MediaType {
	Video,
	Picture,
	Unknown
}

public class Media {
	public ulong ID { get; set; }
	public string Path { get; set; }
	public string RelativePath { get; set; }
	public string Name { get; set; }
	public string Extension { get; set; }
	public ulong Size { get; set; }
	public string Hash { get; set; }
	public MediaType Type { get; set; }

	public Media() {

	}

	public Media(FileInfo fileInfo, string galleryPath) {
		Path = fileInfo.FullName.Replace("\\", "/");
		RelativePath = fileInfo.FullName.Remove(0, galleryPath.Length + 1).Replace("\\", "/");
		Name = fileInfo.Name;
		Extension = fileInfo.Extension;
		Size = (ulong)fileInfo.Length;
	}

	public static bool IsVideo(string fileExtension) {
		switch (fileExtension.ToUpper()) {
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

	public static bool IsPicture(string fileExtension) {
		switch (fileExtension.ToUpper()) {
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

	public static bool IsVideo(Media media) =>
		IsVideo(media.Extension);

	public static bool IsVideo(FileInfo fileInfo) =>
		IsPicture(fileInfo.Extension);

	public static bool IsPicture(Media media) =>
		IsPicture(media.Extension);

	public static bool IsPicture(FileInfo fileInfo) =>
		IsPicture(fileInfo.Extension);
}
