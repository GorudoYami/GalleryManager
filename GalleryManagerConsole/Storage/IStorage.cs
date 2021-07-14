using GalleryManagerConsole.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryManagerConsole.Storage {
    public interface IStorage {
        public bool Setup();
        public bool AddPicture(Media picture);
        public bool AddVideo(Media video);
        public bool AddUnknown(Media media);
        public bool IsIndexed(Media media);
        public ulong TotalPictures();
        public ulong TotalVideos();
        public ulong TotalFiles();
        //public List<Media> GetDuplicates(Media.Type type);
        public Task<bool> Cleanup();
        public bool Delete(List<Media> delete);
        public bool Contains(Media media);
    }
}
