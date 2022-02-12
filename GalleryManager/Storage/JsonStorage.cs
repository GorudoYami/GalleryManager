using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using GalleryManager.Models;

namespace GalleryManager.Storage {
    class JsonStorage : IIndexStorage {
        // TODO
        public bool AddPicture(Media picture) {
            throw new NotImplementedException();
        }

        public bool AddUnknown(Media media) {
            throw new NotImplementedException();
        }

        public bool AddVideo(Media video) {
            throw new NotImplementedException();
        }

        public Task<bool> Cleanup() {
            throw new NotImplementedException();
        }

        public bool Contains(Media media) {
            throw new NotImplementedException();
        }

        public bool Delete(List<Media> delete) {
            throw new NotImplementedException();
        }

        //public List<Media> GetDuplicates(Media.Type type) {
        //    throw new NotImplementedException();
        //}

        public bool IsIndexed(Media media) {
            throw new NotImplementedException();
        }

        public bool Setup() {
            throw new NotImplementedException();
        }

        public long TotalFiles() {
            throw new NotImplementedException();
        }

        public long TotalPictures() {
            throw new NotImplementedException();
        }

        public long TotalVideos() {
            throw new NotImplementedException();
        }
    }
}
