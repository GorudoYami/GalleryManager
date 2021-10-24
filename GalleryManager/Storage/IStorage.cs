using System.Collections.Generic;
using System.Threading.Tasks;

using GalleryManager.Types;

namespace GalleryManager.Storage {
    public interface IStorage {
        public bool Setup();
        public bool AddPicture(Media picture);
        public bool AddVideo(Media video);
        public bool AddUnknown(Media media);
        public bool IsIndexed(Media media);
        public long TotalPictures();
        public long TotalVideos();
        public long TotalFiles();
        //public List<Media> GetDuplicates(Media.Type type);
        public Task<bool> Cleanup();
        public bool Delete(List<Media> delete);
        public bool Contains(Media media);
    }
}
