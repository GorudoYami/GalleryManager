using System;
using System.Collections.Generic;
using System.IO;

using MySql.Data.MySqlClient;

using GalleryManagerConsole.Types;
using System.Threading.Tasks;

namespace GalleryManagerConsole.Storage {
    public class MySqlStorage : IStorage {
        public string ConnectionString { get; set; }
        private readonly List<string> sql;
        private readonly string galleryPath;

        public MySqlStorage(ConnectionInfo info, string galleryPath) {
            CreateConnectionString(info);
            sql = new List<string>();
            using (var reader = new StreamReader("SQL/albums.sql"))
                sql.Add(reader.ReadToEnd());

            using (var reader = new StreamReader("SQL/videos.sql"))
                sql.Add(reader.ReadToEnd());

            using (var reader = new StreamReader("SQL/pictures.sql"))
                sql.Add(reader.ReadToEnd());

            using (var reader = new StreamReader("SQL/unknowns.sql"))
                sql.Add(reader.ReadToEnd());

            this.galleryPath = galleryPath;
        }

        private MySqlConnection GetConnection() =>
            new(ConnectionString);

        private static bool TableExists(string table, MySqlConnection connection) {
            try {
                using MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SHOW TABLES LIKE @Table;";
                cmd.Parameters.Add("@Table", MySqlDbType.VarChar, 64).Value = table;
                using MySqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                    return false;
            }
            catch (Exception e) {
                Console.WriteLine("MySqlDatabase.TableExists()");
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
            }
            return true;
        }

        public void CreateConnectionString(ConnectionInfo info) {
            ConnectionString = "server=" + info.Server + ";port=" + info.Port + ";user=" + info.User;
            if (info.Password != null || info.Password != string.Empty)
                ConnectionString += ";password=" + info.Password;

            ConnectionString += ";database=" + info.Database + ";";
        }

        // TODO:
        // Check for clean transaction handling
        public bool Setup() {
            using MySqlConnection connection = GetConnection();
            bool createAlbums = false;
            bool createVideos = false;
            bool createPictures = false;
            bool createUnknowns = false;

            try {
                connection.Open();

                // Check for table "albums"
                if (!TableExists("albums", connection))
                    createAlbums = true;

                // Check for table "videos"
                if (!TableExists("videos", connection))
                    createVideos = true;

                // Check for table "pictures"   
                if (!TableExists("pictures", connection))
                    createPictures = true;

                // Check for table "unknowns"
                if (!TableExists("unknowns", connection))
                    createUnknowns = true;

                // Create missing tables
                MySqlTransaction transaction = connection.BeginTransaction();

                // Create "albums"
                if (createAlbums) {
                    using MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql[0];
                    cmd.Transaction = transaction;

                    if (cmd.ExecuteNonQuery() == -1) {
                        transaction.Rollback();
                        return false;
                    }
                }

                // Create "videos"
                if (createVideos) {
                    using MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql[1];
                    cmd.Transaction = transaction;

                    if (cmd.ExecuteNonQuery() == -1) {
                        transaction.Rollback();
                        return false;
                    }
                }

                // Create "pictures"
                if (createPictures) {
                    using MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql[2];
                    cmd.Transaction = transaction;

                    if (cmd.ExecuteNonQuery() == -1) {
                        transaction.Rollback();
                        return false;
                    }
                }

                // Create "unknowns"
                if (createUnknowns) {
                    using MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql[3];
                    cmd.Transaction = transaction;

                    if (cmd.ExecuteNonQuery() == -1) {
                        transaction.Rollback();
                        return false;
                    }
                }

                transaction.Commit();
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        // Please check first with IsIndexed()
        public bool AddPicture(Media picture) {
            using MySqlConnection connection = GetConnection();
            picture.Path = picture.Path.Replace("\\", "/");
            picture.RelativePath = picture.RelativePath.Replace("\\", "/");
            try {
                connection.Open();
                using MySqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = "INSERT INTO `pictures` VALUES (null, @RelativePath, @Size, @Format, @Hash, @File);";
                cmd.Parameters.Add("@RelativePath", MySqlDbType.VarChar, 256).Value = picture.RelativePath;
                cmd.Parameters.Add("@File", MySqlDbType.VarChar, 256).Value = MySqlHelper.EscapeString(picture.Name);
                cmd.Parameters.Add("@Format", MySqlDbType.VarChar, 10).Value = MySqlHelper.EscapeString(picture.Format);
                cmd.Parameters.Add("@Size", MySqlDbType.UInt64).Value = picture.Size;
                cmd.Parameters.Add("@Hash", MySqlDbType.VarChar, 128).Value = MySqlHelper.EscapeString(picture.Hash);

                int result = cmd.ExecuteNonQuery();
                if (result == 0 || result == -1)
                    return false;
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        // Please check first with IsIndexed()
        public bool AddVideo(Media video) {
            using MySqlConnection connection = GetConnection();
            video.Path = video.Path.Replace("\\", "/");
            video.RelativePath = video.RelativePath.Replace("\\", "/");
            try {
                connection.Open();
                using MySqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = "INSERT INTO `videos` VALUES (null, @RelativePath, @Size, @Format, @Hash, @File);";
                cmd.Parameters.Add("@RelativePath", MySqlDbType.VarChar, 256).Value = video.RelativePath;
                cmd.Parameters.Add("@File", MySqlDbType.VarChar, 256).Value = MySqlHelper.EscapeString(video.Name);
                cmd.Parameters.Add("@Format", MySqlDbType.VarChar, 10).Value = MySqlHelper.EscapeString(video.Format);
                cmd.Parameters.Add("@Size", MySqlDbType.UInt64).Value = video.Size;
                cmd.Parameters.Add("@Hash", MySqlDbType.VarChar, 128).Value = MySqlHelper.EscapeString(video.Hash);

                int result = cmd.ExecuteNonQuery();
                if (result == 0 || result == -1)
                    return false;
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public bool AddUnknown(Media media) {
            using MySqlConnection connection = GetConnection();
            media.Path = media.Path.Replace("\\", "/");
            media.RelativePath = media.RelativePath.Replace("\\", "/");
            try {
                connection.Open();
                using MySqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = "INSERT INTO `unknowns` VALUES (null, @RelativePath, @Size, @Format, null, @File);";
                cmd.Parameters.Add("@RelativePath", MySqlDbType.VarChar, 256).Value = media.RelativePath;
                cmd.Parameters.Add("@File", MySqlDbType.VarChar, 256).Value = MySqlHelper.EscapeString(media.Name);
                cmd.Parameters.Add("@Format", MySqlDbType.VarChar, 10).Value = MySqlHelper.EscapeString(media.Format);
                cmd.Parameters.Add("@Size", MySqlDbType.UInt64).Value = media.Size;

                int result = cmd.ExecuteNonQuery();
                if (result == 0 || result == -1)
                    return false;
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        // This method checks if a file in the specified location is indexed (has a record in db)
        public bool IsIndexed(Media media) {
            using MySqlConnection connection = GetConnection();
            media.Path = media.Path.Replace("\\", "/");
            media.RelativePath = media.RelativePath.Replace("\\", "/");
            try {
                connection.Open();

                // Check in videos
                using (MySqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM `videos` WHERE `path` = @RelativePath;";
                    cmd.Parameters.Add("@RelativePath", MySqlDbType.VarChar, 256).Value = media.RelativePath;
                    using MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        return true;
                }

                // Check in pictures
                using (MySqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM `pictures` WHERE `path` = @RelativePath;";
                    cmd.Parameters.Add("@RelativePath", MySqlDbType.VarChar, 256).Value = media.RelativePath;
                    using MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        return true;
                }

                // Check in unknowns
                using (MySqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM `unknowns` WHERE `path` = @RelativePath;";
                    cmd.Parameters.Add("@RelativePath", MySqlDbType.VarChar, 256).Value = media.RelativePath;
                    using MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        return true;
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public ulong TotalFiles() {
            using MySqlConnection connection = GetConnection();
            ulong result = 0;

            try {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT COUNT(*) FROM `videos`;";
                    using MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows) {
                        reader.Read();
                        result += reader.GetUInt64(0);
                    }
                }

                using (MySqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT COUNT(*) FROM `pictures`;";
                    using MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows) {
                        reader.Read();
                        result += reader.GetUInt64(0);
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public ulong TotalPictures() {
            using MySqlConnection connection = GetConnection();
            ulong result = 0;
            try {
                connection.Open();
                using MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM `pictures`;";
                using MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    reader.Read();
                    result += reader.GetUInt64(0);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public ulong TotalVideos() {
            using MySqlConnection connection = GetConnection();
            ulong result = 0;
            try {
                connection.Open();
                using MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM `videos`;";
                using MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    reader.Read();
                    result += reader.GetUInt64(0);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public List<Media> GetDuplicates() {
            throw new NotImplementedException();
        }

        public async Task<bool> Cleanup() {
            using MySqlConnection connection = GetConnection();
            try {
                connection.Open();
                using MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `pictures`;";
                using var reader = await cmd.ExecuteReaderAsync();
                List<string> trashList = new();
                while (reader.Read()) {
                    string path = galleryPath + reader["path"];
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public bool Delete(List<Media> delete) {
            throw new NotImplementedException();
        }

        // This method checks if the gallery already contains picture/video with specified hash
        public bool Contains(Media media) {
            using MySqlConnection connection = GetConnection();
            try {
                connection.Open();

                // Check in videos
                using (MySqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM `videos` WHERE `hash` LIKE @Hash;";
                    cmd.Parameters.Add("@Hash", MySqlDbType.VarChar, 256).Value = MySqlHelper.EscapeString(media.Hash);
                    using MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        return true;
                }

                // Check in pictures
                using (MySqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM `pictures` WHERE `hash` LIKE @Hash;";
                    cmd.Parameters.Add("@Hash", MySqlDbType.VarChar, 256).Value = MySqlHelper.EscapeString(media.Hash);
                    using MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        return true;
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
