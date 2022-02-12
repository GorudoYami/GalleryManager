using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryManager.Models {
    public static class Units {
        public const long KB = 1024;
        public const long MB = KB * KB;
        public const long GB = MB * KB;
        public const long TB = GB * KB;

        public static string BytesToHumanReadable(long bytes) {
            return bytes switch {
                (< KB) => $"{bytes}B",
                (>= KB) and (< MB) => $"{bytes / KB}KB",
                (>= MB) and (< GB) => $"{bytes / MB}MB",
                (>= GB) and (< TB) => $"{bytes / MB}GB",
                (>= TB) => $"{bytes / TB}"
            };
        }
    }
}
