using ImporterStandalone.Storage;
using ImporterStandalone.Types;
using System;
using System.Collections.Generic;

namespace ImporterStandalone {
    class Program {
        private static MySqlStorage storage;
        private static Importer importer;
        private static Dictionary<string, Progress<int>> progress;
        static void Main(string[] args) {
            string server, user, password, database, path;
            bool listen = false;
            // Get database/gallery info
            if (args.Length == 5) {
                server = args[0];
                user = args[1];
                password = args[2];
                database = args[3];
                path = args[4];
            }
            else {
                Console.Write("Host: ");
                server = Console.ReadLine();
                Console.Write("User: ");
                user = Console.ReadLine();
                Console.Write("Password: ");
                password = Console.ReadLine();
                Console.Write("Database: ");
                database = Console.ReadLine();
                Console.Write("Gallery path: ");
                path = Console.ReadLine();
                Console.Write("Listening mode Y/N: ");
                string input = Console.ReadLine();
                if (input.ToUpper() == "Y")
                    listen = true;
                else
                    listen = false;
            }

            // Create connection info for database
            string[] host = server.Split(':');
            ConnectionInfo connectionInfo = new ConnectionInfo {
                Server = host[0],
                Port = host[1],
                User = user,
                Password = password,
                Database = database
            };

            // Setup database
            storage = new MySqlStorage(connectionInfo);
            if (storage.Setup())
                Console.WriteLine("Storage setup successful!");
            else {
                Console.WriteLine("You fucked up something and storage setup failed.");
                return;
            }

            // Setup importer
            progress = new Dictionary<string, Progress<int>> {
                ["newVideos"] = new Progress<int>(),
                ["newPictures"] = new Progress<int>(),
                ["pictureCount"] = new Progress<int>(),
                ["videoCount"] = new Progress<int>(),
                ["overall"] = new Progress<int>()
            };
            importer = new Importer(storage, path, progress);

            Console.WriteLine("Importer initialized.");
            if (listen) {
                Console.WriteLine("Listening mode is active...");
                foreach (string driveName in Environment.GetLogicalDrives())
                    Console.WriteLine(driveName);
            }
            else {
                Console.WriteLine("Type in \"HELP\" for command list.");
                // Handle commands
                bool exit = false;
                while (!exit) {
                    string cmd = Console.ReadLine();
                    switch (cmd.ToUpper()) {
                        case "EXIT":
                        case "QUIT":
                            exit = true;
                            break;
                    }
                }
            }
        }
    }
}
