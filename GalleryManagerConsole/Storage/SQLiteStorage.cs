﻿using GalleryManagerConsole.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryManagerConsole.Storage {
    class SQLiteStorage : IStorage {
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

        public ulong TotalFiles() {
            throw new NotImplementedException();
        }

        public ulong TotalPictures() {
            throw new NotImplementedException();
        }

        public ulong TotalVideos() {
            throw new NotImplementedException();
        }
    }
}
