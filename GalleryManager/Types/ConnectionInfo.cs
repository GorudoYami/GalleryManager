
namespace GalleryManager.Types {
    public enum ConnectionType {
        SQLite,
        MySQL
    }

    public class ConnectionInfo {
        public ConnectionType Type { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string ConnectionString { get; set; }

        public void CreateConnectionString() {
            ConnectionString = "server=" + Server + ";port=" + Port + ";user=" + User;
            if (Password != null || Password != string.Empty)
                ConnectionString += ";password=" + Password;

            ConnectionString += ";database=" + Database + ";";
        }
    }
}
