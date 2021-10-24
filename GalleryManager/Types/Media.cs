using System.IO;

namespace GalleryManager.Types {
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
        public string Format { get; set; }
        public ulong Size { get; set; }
        public string Hash { get; set; }
        public MediaType Type { get; set; }
        public static bool IsVideo(FileInfo file) {
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
        public static bool IsPicture(FileInfo file) {
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
        public static bool IsVideo(Media media) {
            switch (media.Format.ToUpper()) {
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
        public static bool IsPicture(Media media) {
            switch (media.Format.ToUpper()) {
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
