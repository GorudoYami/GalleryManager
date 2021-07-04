using System;
using System.Timers;

namespace IndexerStandalone {
    public class Program {
        private static DateTime timestamp;
        private static double progressValue;
        private static bool autoUpdate;
        private static Timer timer;
        private static Indexer indexer;
        private static uint updateInterval;
        public static void Main(string[] args) {
            // Variable setup
            string server, user, password, database, path;
            Progress<double> progress = new(UpdateProgress);
            timestamp = DateTime.Now;
            autoUpdate = false;
            updateInterval = 900000; // 15 minutes
            timer = new Timer(updateInterval);
            timer.Elapsed += Update;
            timer.AutoReset = true;
            timer.Enabled = false;

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
                Console.Clear();
            }

            // Create connection info for database
            string[] host = server.Split(':');
            ConnectionInfo connectionInfo = new() {
                Server = host[0],
                Port = host[1],
                User = user,
                Password = password,
                Database = database
            };

            // Setup database
            MySqlStorage storage = new(connectionInfo);

            if (storage.Setup())
                Console.WriteLine("Storage setup successful!");
            else {
                Console.WriteLine("You fucked up something and storage setup failed.");
                return;
            }

            // Create indexer
            indexer = new Indexer(storage, path, progress);

            Console.WriteLine("Indexer initialized.");
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
                    case "START":
                        indexer.Start();
                        break;
                    case "STOP":
                        indexer.Stop();
                        break;
                    case "STATUS":
                        if (indexer.Working) {
                            Console.WriteLine("Indexer is working");
                            Console.WriteLine("Progress: " + progressValue + "%");
                        }
                        else
                            Console.WriteLine("Indexer isn't working");
                        break;
                    case "AUTOUPDATE":
                        autoUpdate = !autoUpdate;
                        if (autoUpdate)
                            Console.WriteLine("AutoUpdate is turned on");
                        else
                            Console.WriteLine("AutoUpdate is turned off");
                        break;
                    case "UPDATEINTERVAL":
                        Console.WriteLine("Current interval: " + updateInterval / 60000 + " min");
                        Console.Write("Enter new interval (minutes): ");
                        if (uint.TryParse(Console.ReadLine(), out updateInterval)) {
                            Console.WriteLine("Success");
                            updateInterval *= 60000;
                            if (updateInterval == 0)
                                updateInterval = 900000;
                            timer.Interval = updateInterval;
                        }
                        else
                            Console.WriteLine("Failed");
                        break;
                    case "TOTALVIDEOS":
                        Console.WriteLine("Total videos indexed: " + storage.TotalVideos());
                        break;
                    case "TOTALPICTURES":
                        Console.WriteLine("Total pictures indexed: " + storage.TotalPictures());
                        break;
                    case "TOTALFILES":
                        Console.WriteLine("Total files in gallery: " + storage.TotalFiles());
                        break;
                    case "CLEAR":
                        Console.Clear();
                        break;
                    case "HELP":
                        Console.WriteLine("Available commands:");
                        Console.WriteLine("EXIT/QUIT - stops the indexer and exits");
                        Console.WriteLine("START - starts the indexer (if not started already)");
                        Console.WriteLine("STOP - stops the indexer (if not stopped already)");
                        Console.WriteLine("STATUS - displays indexer status and its progress");
                        Console.WriteLine("AUTOUPDATE - toggles auto update of the gallery");
                        Console.WriteLine("UPDATEINTERVAL - prompts for a new auto update interval");
                        Console.WriteLine("TOTALVIDEOS - displays total amount of indexed videos");
                        Console.WriteLine("TOTALPICTURES - displays total amount of indexed pictures");
                        Console.WriteLine("TOTALFILES - displays total amount of files detected in gallery (not all of them may be indexed)");
                        Console.WriteLine("CLEAR - clears the console");
                        Console.WriteLine("HELP - displays this list");
                        break;
                    default:
                        Console.WriteLine("Unknown command. Type \"help\" for command list.");
                        break;
                }
            }

            // Cleanup
            indexer.Stop();
            timer.Stop();
        }

        private static void UpdateProgress(double value) {
            progressValue = value;
        }

        private static void Update(object sender, ElapsedEventArgs e) {
            if (!indexer.Working && DateTime.Now.Subtract(timestamp).TotalMinutes >= 15) {
                indexer.Start();
                timestamp = DateTime.Now;
            }
        }
    }
}
