using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;

using Microsoft.Data.Sqlite;

using GalleryManager.Models;
using System.Diagnostics;
using MySqlConnector;

namespace GalleryManager.Storage {
	public class DatabaseStorage : IIndexStorage {
		private readonly ConnectionInfo connectionInfo;
		// |R| public string ConnectionString { get; set; }
		private readonly List<string> sql;
		private readonly string galleryPath;

		public DatabaseStorage(ConnectionInfo connectionInfo, string galleryPath) {
			// |R| CreateConnectionString(info);
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
			this.connectionInfo = connectionInfo;
		}

		private DbConnection GetConnection() =>
			(connectionInfo.Type == ConnectionType.MySQL) ? new MySqlConnection(connectionInfo.ConnectionString) : new SqliteConnection("Data Source=indexes.db");

		private bool TableExists(string table, DbConnection connection) {
			try {
				using var cmd = connection.CreateCommand();
				cmd.CommandText = "SHOW TABLES LIKE @Table";

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[0].ParameterName = "@Table";
				cmd.Parameters[0].Value = table;

				using var reader = cmd.ExecuteReader();
				if (!reader.HasRows)
					return false;
			}
			catch (Exception e) {
				Debug.WriteLine(e.GetType().ToString().ToString());
				Debug.WriteLine(e.Message);
			}
			return true;
		}

		// TODO:
		// Check for clean transaction handling
		public bool Setup() {
			using var connection = GetConnection();
			bool createAlbums = false;
			bool createVideos = false;
			bool createPictures = false;
			bool createUnknowns = false;

			try {
				connection.Open();

				// Set charset cause for some reason it started crashing without it
				using (var cmd = connection.CreateCommand()) {
					cmd.CommandText = "SET NAMES 'utf8'";
					cmd.ExecuteNonQuery();
				}

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
				var transaction = connection.BeginTransaction();

				// Create "albums"
				if (createAlbums) {
					using var cmd = connection.CreateCommand();
					cmd.CommandText = sql[0];
					cmd.Transaction = transaction;

					if (cmd.ExecuteNonQuery() == -1) {
						transaction.Rollback();
						return false;
					}
				}

				// Create "videos"
				if (createVideos) {
					using var cmd = connection.CreateCommand();
					cmd.CommandText = sql[1];
					cmd.Transaction = transaction;

					if (cmd.ExecuteNonQuery() == -1) {
						transaction.Rollback();
						return false;
					}
				}

				// Create "pictures"
				if (createPictures) {
					using var cmd = connection.CreateCommand();
					cmd.CommandText = sql[2];
					cmd.Transaction = transaction;

					if (cmd.ExecuteNonQuery() == -1) {
						transaction.Rollback();
						return false;
					}
				}

				// Create "unknowns"
				if (createUnknowns) {
					using var cmd = connection.CreateCommand();
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
				Debug.WriteLine(e.GetType().ToString());
				Debug.WriteLine(e.Message);
				return false;
			}
			return true;
		}

		// Please check first with IsIndexed()
		// DRY
		public bool AddPicture(Media picture) {
			using DbConnection connection = GetConnection();
			picture.Path = picture.Path.Replace("\\", "/");
			picture.RelativePath = picture.RelativePath.Replace("\\", "/");
			try {
				connection.Open();
				using var cmd = connection.CreateCommand();

				cmd.CommandText = "INSERT INTO `pictures` VALUES (null, @RelativePath, @Size, @Format, @Hash, @File);";

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[0].ParameterName = "@RelativePath";
				cmd.Parameters[0].Value = picture.RelativePath;

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[1].ParameterName = "@Size";
				cmd.Parameters[1].Value = picture.Size;

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[2].ParameterName = "@Format";
				cmd.Parameters[2].Value = picture.Extension;

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[3].ParameterName = "@Hash";
				cmd.Parameters[3].Value = picture.Hash;

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[4].ParameterName = "@File";
				cmd.Parameters[4].Value = picture.Name;

				if (cmd.ExecuteNonQuery() != 1)
					return false;
			}
			catch (Exception e) {
				Debug.WriteLine(e.GetType().ToString());
				Debug.WriteLine(e.Message);
				return false;
			}
			return true;
		}

		// Please check first with IsIndexed()
		// DRY
		public bool AddVideo(Media video) {
			using DbConnection connection = GetConnection();
			video.Path = video.Path.Replace("\\", "/");
			video.RelativePath = video.RelativePath.Replace("\\", "/");
			try {
				connection.Open();
				using var cmd = connection.CreateCommand();

				cmd.CommandText = "INSERT INTO `videos` VALUES (null, @RelativePath, @Size, @Format, @Hash, @File);";

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[0].ParameterName = "@RelativePath";
				cmd.Parameters[0].Value = video.RelativePath;

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[1].ParameterName = "@Size";
				cmd.Parameters[1].Value = video.Size;

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[2].ParameterName = "@Format";
				cmd.Parameters[2].Value = video.Extension;

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[3].ParameterName = "@Hash";
				cmd.Parameters[3].Value = video.Hash;

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[4].ParameterName = "@File";
				cmd.Parameters[4].Value = video.Name;

				if (cmd.ExecuteNonQuery() != 1)
					return false;
			}
			catch (Exception e) {
				Debug.WriteLine(e.GetType().ToString());
				Debug.WriteLine(e.Message);
				return false;
			}
			return true;
		}

		// DRY
		public bool AddUnknown(Media media) {
			using DbConnection connection = GetConnection();
			media.Path = media.Path.Replace("\\", "/");
			media.RelativePath = media.RelativePath.Replace("\\", "/");
			try {
				connection.Open();
				using var cmd = connection.CreateCommand();

				cmd.CommandText = "INSERT INTO `unknowns` VALUES (null, @RelativePath, @Size, @Format, null, @File);";

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[0].ParameterName = "@RelativePath";
				cmd.Parameters[0].Value = media.RelativePath;

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[1].ParameterName = "@Size";
				cmd.Parameters[1].Value = media.Size;

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[2].ParameterName = "@Format";
				cmd.Parameters[2].Value = media.Extension;

				cmd.Parameters.Add(cmd.CreateParameter());
				cmd.Parameters[3].ParameterName = "@File";
				cmd.Parameters[3].Value = media.Name;

				int result = cmd.ExecuteNonQuery();
				if (result == 0 || result == -1)
					return false;
			}
			catch (Exception e) {
				Debug.WriteLine(e.GetType().ToString());
				Debug.WriteLine(e.Message);
				return false;
			}
			return true;
		}

		// DRY?
		// This method checks if a file in the specified location is indexed (has a record in db)
		public bool IsIndexed(Media media) {
			using var connection = GetConnection();
			media.Path = media.Path.Replace("\\", "/");
			media.RelativePath = media.RelativePath.Replace("\\", "/");
			try {
				connection.Open();

				// Check in videos
				using (var cmd = connection.CreateCommand()) {
					cmd.CommandText = "SELECT * FROM `videos` WHERE `path` = @RelativePath;";

					cmd.Parameters.Add(cmd.CreateParameter());
					cmd.Parameters[0].ParameterName = "@RelativePath";
					cmd.Parameters[0].Value = media.RelativePath;

					using var reader = cmd.ExecuteReader();
					if (reader.HasRows)
						return true;
				}

				// Check in pictures
				using (var cmd = connection.CreateCommand()) {
					cmd.CommandText = "SELECT * FROM `pictures` WHERE `path` = @RelativePath;";

					cmd.Parameters.Add(cmd.CreateParameter());
					cmd.Parameters[0].ParameterName = "@RelativePath";
					cmd.Parameters[0].Value = media.RelativePath;

					using var reader = cmd.ExecuteReader();
					if (reader.HasRows)
						return true;
				}

				// Check in unknowns
				using (var cmd = connection.CreateCommand()) {
					cmd.CommandText = "SELECT * FROM `unknowns` WHERE `path` = @RelativePath;";

					cmd.Parameters.Add(cmd.CreateParameter());
					cmd.Parameters[0].ParameterName = "@RelativePath";
					cmd.Parameters[0].Value = media.RelativePath;

					using var reader = cmd.ExecuteReader();
					if (reader.HasRows)
						return true;
				}
			}
			catch (Exception e) {
				Debug.WriteLine(e.GetType().ToString());
				Debug.WriteLine(e.Message);
			}
			return false;
		}

		public long TotalFiles() {
			using DbConnection connection = GetConnection();
			long result = 0;

			try {
				connection.Open();

				using (var cmd = connection.CreateCommand()) {
					cmd.CommandText = "SELECT COUNT(*) FROM `videos`;";
					using var reader = cmd.ExecuteReader();
					if (reader.HasRows) {
						reader.Read();
						result += reader.GetInt64(0);
					}
				}

				using (var cmd = connection.CreateCommand()) {
					cmd.CommandText = "SELECT COUNT(*) FROM `pictures`;";
					using var reader = cmd.ExecuteReader();
					if (reader.HasRows) {
						reader.Read();
						result += reader.GetInt64(0);
					}
				}
			}
			catch (Exception e) {
				Debug.WriteLine(e.GetType().ToString());
				Debug.WriteLine(e.Message);
			}
			return result;
		}

		public long TotalPictures() {
			using DbConnection connection = GetConnection();
			long result = 0;
			try {
				connection.Open();
				using var cmd = connection.CreateCommand();
				cmd.CommandText = "SELECT COUNT(*) FROM `pictures`;";
				using var reader = cmd.ExecuteReader();
				if (reader.HasRows) {
					reader.Read();
					result += reader.GetInt64(0);
				}
			}
			catch (Exception e) {
				Debug.WriteLine(e.GetType().ToString());
				Debug.WriteLine(e.Message);
			}
			return result;
		}

		public long TotalVideos() {
			using DbConnection connection = GetConnection();
			long result = 0;
			try {
				connection.Open();
				using var cmd = connection.CreateCommand();
				cmd.CommandText = "SELECT COUNT(*) FROM `videos`;";
				using var reader = cmd.ExecuteReader();
				if (reader.HasRows) {
					reader.Read();
					result += reader.GetInt64(0);
				}
			}
			catch (Exception e) {
				Debug.WriteLine(e.GetType().ToString());
				Debug.WriteLine(e.Message);
			}
			return result;
		}

		public List<Media> GetDuplicates() {
			throw new NotImplementedException();
		}

		public async Task<bool> Cleanup() {
			List<int> trashList = new();
			string[] tables = {
				"`pictures`",
				"`videos`",
				"`unknowns`"
			};

			using DbConnection connection = GetConnection();
			try {
				connection.Open();
			}
			catch (Exception e) {
				Debug.WriteLine(e.GetType().ToString());
				Debug.WriteLine(e.Message);
				return false;
			}

			int deletedCount = 0;
			foreach (string table in tables) {
				using (var cmd = connection.CreateCommand()) {
					cmd.CommandText = "SELECT * FROM " + table + ";";
					using var reader = await cmd.ExecuteReaderAsync();

					// Get all non-existing files
					while (reader.Read()) {
						string path = galleryPath + Convert.ToString(reader["path"]);
						if (!File.Exists(path))
							trashList.Add(Convert.ToInt32(reader["id"]));
					}
				}

				// Delete them
				foreach (int id in trashList) {
					using var cmd = connection.CreateCommand();
					cmd.CommandText = "DELETE FROM " + table + " WHERE id = @Id;";

					cmd.Parameters.Add(cmd.CreateParameter());
					cmd.Parameters[0].ParameterName = "@Id";
					cmd.Parameters[0].Value = id;

					if (await cmd.ExecuteNonQueryAsync() != 1)
						return false;
				}
				deletedCount += trashList.Count;
				trashList.Clear();
			}
			Debug.WriteLine("Cleanup complete!");
			Debug.WriteLine("Deleted records: " + deletedCount);
			return true;
		}

		public bool Delete(List<Media> delete) {
			throw new NotImplementedException();
		}

		// This method checks if the gallery already contains picture/video with specified hash
		public bool Contains(Media media) {
			using DbConnection connection = GetConnection();
			try {
				connection.Open();

				// Check in videos
				using (var cmd = connection.CreateCommand()) {
					cmd.CommandText = "SELECT * FROM `videos` WHERE `hash` LIKE @Hash;";

					cmd.Parameters.Add(cmd.CreateParameter());
					cmd.Parameters[0].ParameterName = "@Hash";
					cmd.Parameters[0].Value = media.Hash;

					using var reader = cmd.ExecuteReader();
					if (reader.HasRows)
						return true;
				}

				// Check in pictures
				using (var cmd = connection.CreateCommand()) {
					cmd.CommandText = "SELECT * FROM `pictures` WHERE `hash` LIKE @Hash;";

					cmd.Parameters.Add(cmd.CreateParameter());
					cmd.Parameters[0].ParameterName = "@Hash";
					cmd.Parameters[0].Value = media.Hash;

					using var reader = cmd.ExecuteReader();
					if (reader.HasRows)
						return true;
				}
			}
			catch (Exception e) {
				Debug.WriteLine(e.GetType().ToString());
				Debug.WriteLine(e.Message);
			}
			return false;
		}
	}
}
