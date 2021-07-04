using ImporterStandalone.Types;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImporterStandalone.Storage {
    public class MySqlStorage {
        public string ConnectionString { get; set; }
        private readonly List<string> tables;
        public MySqlStorage(ConnectionInfo info) {
            CreateConnectionString(info);
            tables = new List<string>();
            using (var reader = new StreamReader("SQL/albums.sql"))
                tables.Add(reader.ReadToEnd());
            using (var reader = new StreamReader("SQL/videos.sql"))
                tables.Add(reader.ReadToEnd());
            using (var reader = new StreamReader("SQL/pictures.sql"))
                tables.Add(reader.ReadToEnd());
            using (var reader = new StreamReader("SQL/unknowns.sql"))
                tables.Add(reader.ReadToEnd());
        }

        private MySqlConnection GetConnection() {
            return new MySqlConnection(ConnectionString);
        }

        private bool TableExists(string table, MySqlConnection connection) {
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
                    cmd.CommandText = tables[0];
                    cmd.Transaction = transaction;

                    if (cmd.ExecuteNonQuery() == -1) {
                        transaction.Rollback();
                        return false;
                    }
                }

                // Create "videos"
                if (createVideos) {
                    using MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = tables[1];
                    cmd.Transaction = transaction;

                    if (cmd.ExecuteNonQuery() == -1) {
                        transaction.Rollback();
                        return false;
                    }
                }

                // Create "pictures"
                if (createPictures) {
                    using MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = tables[2];
                    cmd.Transaction = transaction;

                    if (cmd.ExecuteNonQuery() == -1) {
                        transaction.Rollback();
                        return false;
                    }
                }

                // Create "unknowns"
                if (createUnknowns) {
                    using MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = tables[3];
                    cmd.Transaction = transaction;

                    if (cmd.ExecuteNonQuery() == -1) {
                        transaction.Rollback();
                        return false;
                    }
                }

                transaction.Commit();
            }
            catch (Exception e) {
                Console.WriteLine("MySqlDatabase.Setup()");
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        // Please check first with IsIndexed()
        public bool AddPicture(Media picture) {
            using MySqlConnection connection = GetConnection();
            try {
                connection.Open();
                using MySqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = "INSERT INTO `pictures` VALUES (null, @Path, @Size, @Format, @Hash, @File);";
                cmd.Parameters.Add("@Path", MySqlDbType.VarChar, 256).Value = picture.Path;
                cmd.Parameters.Add("@File", MySqlDbType.VarChar, 256).Value = MySqlHelper.EscapeString(picture.Name);
                cmd.Parameters.Add("@Format", MySqlDbType.VarChar, 5).Value = MySqlHelper.EscapeString(picture.Format);
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
            try {
                connection.Open();
                using MySqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = "INSERT INTO `videos` VALUES (null, @Path, @Size, @Format, @Hash, @File);";
                cmd.Parameters.Add("@Path", MySqlDbType.VarChar, 256).Value = video.Path;
                cmd.Parameters.Add("@File", MySqlDbType.VarChar, 256).Value = MySqlHelper.EscapeString(video.Name);
                cmd.Parameters.Add("@Format", MySqlDbType.VarChar, 5).Value = MySqlHelper.EscapeString(video.Format);
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

        public bool AddAlbum(Album album) {
            return true;
        }

        public bool AddUnknown(Media media) {
            using MySqlConnection connection = GetConnection();
            try {
                connection.Open();
                using MySqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = "INSERT INTO `unknowns` VALUES (null, @Path, @Size, @Format, null, @File);";
                cmd.Parameters.Add("@Path", MySqlDbType.VarChar, 256).Value = media.Path;
                cmd.Parameters.Add("@File", MySqlDbType.VarChar, 256).Value = MySqlHelper.EscapeString(media.Name);
                cmd.Parameters.Add("@Format", MySqlDbType.VarChar, 5).Value = MySqlHelper.EscapeString(media.Format);
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

        public bool IsIndexed(Media media) {
            using MySqlConnection connection = GetConnection();
            try {
                connection.Open();

                // Check in videos
                using (MySqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM `videos` WHERE path LIKE @Path;";
                    cmd.Parameters.Add("@Path", MySqlDbType.VarChar, 256).Value = MySqlHelper.EscapeString(media.Path);
                    using MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        return true;
                }

                // Check in pictures
                using (MySqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM `pictures` WHERE path LIKE @Path;";
                    cmd.Parameters.Add("@Path", MySqlDbType.VarChar, 256).Value = MySqlHelper.EscapeString(media.Path);
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

        public List<Media> GetDuplicates(Media.Type type) {
            using MySqlConnection connection = GetConnection();
            List<Media> result = new List<Media>();

            try {
                connection.Open();
                using MySqlCommand cmd = connection.CreateCommand();
                if (type == Media.Type.PICTURE) {
                    using var sr = new StreamReader("SQL/duplicated_pictures.sql");
                    cmd.CommandText = sr.ReadToEnd();
                }
                else {
                    using var sr = new StreamReader("SQL/duplicated_videos.sql");
                    cmd.CommandText = sr.ReadToEnd();
                }

                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    result.Add(new Media {
                        ID = Convert.ToUInt64(reader["id"]),
                        Path = Convert.ToString(reader["path"]),
                        Size = Convert.ToUInt64(reader["size"]),
                        Format = Convert.ToString(reader["format"]),
                        Hash = Convert.ToString(reader["hash"]),
                        Name = Convert.ToString(reader["file"])
                    });
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                return null;
            }

            return result;
        }

        public bool Cleanup() {
            return false;
        }

        public bool Delete(List<Media> delete) {
            using MySqlConnection connection = GetConnection();
            try {
                connection.Open();
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                return false;
            }

            MySqlTransaction transaction = connection.BeginTransaction();
            try {
                foreach (Media media in delete) {
                    using MySqlCommand cmd = connection.CreateCommand();
                    if (IsVideo(media))
                        cmd.CommandText = "DELETE FROM `videos` WHERE `path` = @Path;";
                    else
                        cmd.CommandText = "DELETE FROM `pictures` WHERE `path` = @Path;";
                    cmd.Transaction = transaction;
                    cmd.Parameters.Add("@Path", MySqlDbType.VarChar, 256).Value = media.Path;

                    if (cmd.ExecuteNonQuery() == 0)
                        throw new Exception();
                    File.Delete(media.Path);
                }
                transaction.Commit();
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                transaction.Rollback();
                return false;
            }
            return true;
        }

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

        private bool IsVideo(Media media) {
            string ext = media.Format.ToUpper();
            switch (ext) {
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

        private bool IsPicture(Media media) {
            string ext = media.Format.ToUpper();
            switch (ext) {
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
                case ".CGM":
                    return true;
                default:
                    return false;
            }
        }
    }
}
