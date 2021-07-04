using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryManager.Types {
    public class Media {
        public enum Type {
            PICTURE,
            VIDEO
        }
        public ulong ID { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public ulong Size { get; set; }
        public string Hash { get; set; }
    }
}
