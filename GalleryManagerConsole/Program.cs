using System;
using System.Timers;

using GalleryManagerConsole.Types;
using GalleryManagerConsole.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace GalleryManagerConsole {
    public class Program {
        //private static DateTime timestamp;
        //private static double progressValue;
        //private static bool autoUpdate;
        //private static Timer timer;
        //private static uint updateInterval;

        private static Indexer indexer;
        private static Importer importer;
        private static List<DriveInfo> driveList;
        //private static List<DriveInfo> driveIgnoreList;

        private static Timer driveTimer;
        public static void Main(string[] args) =>
            MainAsync(args).GetAwaiter().GetResult();

        public static async Task MainAsync(string[] args) {
            // Variable setup
            string server, user, password, database, path;

            // Setup drive update timer
            driveTimer = new();
            driveTimer.Elapsed += UpdateDrives;
            driveTimer.Interval = 5000;
            UpdateDrives(null, null);

            //driveIgnoreList = new();

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

            // Setup indexer
            indexer = new Indexer(storage, path);
            Console.WriteLine("Indexer initialized.");

            // Setup importer
            importer = new Importer(storage, path);
            Console.WriteLine("Importer initialized.");

            Console.WriteLine("Type in 'help' for command list.");

            // Handle commands
            bool exit = false;
            while (!exit) {
                string[] cmd = Console.ReadLine().Split(' ');
                switch (cmd[0].ToUpper()) {
                    case "EXIT":
                    case "QUIT":
                        exit = true;
                        break;
                    case "INDEXER":
                        if (cmd.Length > 1) {
                            switch (cmd[1].ToUpper()) {
                                case "START":
                                    indexer.Start();
                                    Console.WriteLine("Indexer started!");
                                    break;
                                case "STOP":
                                    indexer.Stop();
                                    Console.WriteLine("Indexer stopped!");
                                    break;
                                case "STATUS":
                                    Console.WriteLine("Indexer is now " + (indexer.Working ? "ACTIVE" : "INACTIVE"));
                                    if (indexer.Working) {
                                        Console.WriteLine("Total files to index: " + indexer.FileCount);
                                        Console.WriteLine("Files indexed: " + indexer.FilesIndexed);
                                        Console.WriteLine("Indexed percent: " + (indexer.FilesIndexed / indexer.FileCount * 100.0));
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Unknown argument!");
                                    break;
                            }
                        }
                        else
                            Console.WriteLine("Too few arguments!");
                        break;
                    case "IMPORTER":
                        if (cmd.Length > 1) {
                            switch (cmd[1].ToUpper()) {
                                case "DRIVE":
                                    if (cmd.Length > 2) {
                                        switch(cmd[2].ToUpper()) {
                                            case "ADD": {
                                                if (cmd.Length > 3) {
                                                    if (uint.TryParse(cmd[3], out uint index)) {
                                                        importer.AddDrive(driveList[(int)index]);
                                                        Console.WriteLine("Drive '" + driveList[(int)index].Name + "' has been added!");
                                                    }
                                                    else
                                                        Console.WriteLine("Invalid drive ID!");
                                                }
                                                break;
                                            }
                                            case "REMOVE": {
                                                if (uint.TryParse(cmd[3], out uint index)) {
                                                    await importer.RemoveDriveAsync(driveList[(int)index]);
                                                    Console.WriteLine("Drive '" + driveList[(int)index].Name + "' has been removed!");
                                                }
                                                else
                                                    Console.WriteLine("Invalid drive id!");
                                                break;
                                            }
                                            case "LIST":
                                                UpdateDrives(null, null);
                                                Console.WriteLine("Available drives:");
                                                foreach (DriveInfo drive in driveList) {
                                                    Console.Write(driveList.IndexOf(drive) + " | Path: " + drive.Name + " ");
                                                    Console.WriteLine();
                                                }
                                                Console.WriteLine("Drives being processed:");
                                                foreach (DriveInfo drive in driveList) {
                                                    if (importer.IsAdded(drive)) {
                                                        Console.Write(driveList.IndexOf(drive) + " | Path: " + drive.Name + " ");
                                                        Console.WriteLine();
                                                    }
                                                }
                                                break;
                                            default:
                                                Console.WriteLine("Unknown argument: '" + cmd[2] + "'");
                                                break;
                                        }
                                    }
                                    else
                                        Console.WriteLine("Too few arguments!");
                                    break;
                                case "DIRECTORY":
                                    if (cmd.Length > 2) {
                                        switch (cmd[2].ToUpper()) {
                                            case "ADD":
                                                if (cmd.Length > 3) {
                                                    if (Directory.Exists(cmd[3])) {
                                                        importer.AddDirectory(new DirectoryInfo(cmd[3]));
                                                        Console.WriteLine("Directory '" + cmd[3] + "' has been added!");
                                                    }
                                                    else
                                                        Console.WriteLine("Directory doesn't exist!");
                                                }
                                                break;
                                            case "REMOVE":
                                                if (cmd.Length > 3) {
                                                    if (Directory.Exists(cmd[3]) && importer.IsAdded(new DirectoryInfo(cmd[3]))) {
                                                        await importer.RemoveDirectoryAsync(new DirectoryInfo(cmd[3]));
                                                        Console.WriteLine("Directory '" + cmd[3] + "' has been removed!");
                                                    }
                                                }
                                                break;
                                            default:
                                                Console.WriteLine("Unknown argument: '" + cmd[2] + "'");
                                                break;
                                        }
                                    }
                                    else
                                        Console.WriteLine("Too few arguments!");
                                    break;
                                case "REMOVEALL":
                                    importer.RemoveAll();
                                    Console.WriteLine("Aborted all imports!");
                                    break;
                                case "STATUS":
                                    UpdateDrives(null, null);
                                    Console.WriteLine("Available drives:");
                                    foreach (DriveInfo drive in driveList) { 
                                        Console.Write(driveList.IndexOf(drive) + " | Mount point: " + drive.Name + " ");
                                        //Console.Write("Size: " + drive.TotalSize / 1073741824 + "GB ");
                                        //Console.Write("Used: " + (drive.TotalSize - drive.TotalFreeSpace) / 1073741824.0 + " GB (" + (drive.TotalSize - drive.TotalFreeSpace) / drive.TotalSize * 100.0 + "%)");
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("Drives being processed:");
                                    foreach (DriveInfo drive in driveList) {
                                        if (importer.IsAdded(drive)) {
                                            Console.Write(driveList.IndexOf(drive) + " | Mount point: " + drive.Name + " ");
                                            //Console.Write("Size: " + drive.TotalSize / 1073741824 + "GB ");
                                            //Console.Write("Used: " + (drive.TotalSize - drive.TotalFreeSpace) / 1073741824.0 + " GB (" + (drive.TotalSize - drive.TotalFreeSpace) / drive.TotalSize * 100.0 + "%)");
                                            Console.WriteLine();
                                        }
                                    }
                                    break;
                                case "START":
                                    if (await importer.ImportReadyAsync()) {
                                        Console.WriteLine("Beginning importing process...");
                                        importer.StartImport();
                                        break;
                                    }
                                    else {
                                        Console.WriteLine("Imports are not ready yet!");
                                        break;
                                    }
                                default:
                                    Console.WriteLine("Unknown argument!");
                                    break;
                            }
                        }
                        else
                            Console.WriteLine("Too few arguments!");
                        break;
                    case "CLEAR":
                        Console.Clear();
                        break;
                    case "HELP":
                        Console.WriteLine("Available commands:");
                        Console.WriteLine("INDEXER START/STOP/STATUS");
                        Console.WriteLine("IMPORTER DRIVE ADD/REMOVE/LIST");
                        Console.WriteLine("IMPORTER DIRECTORY ADD/REMOVE");
                        Console.WriteLine("IMPORTER START/STATUS/REMOVEALL");
                        Console.WriteLine("EXIT/QUIT");
                        Console.WriteLine("CLEAR");
                        Console.WriteLine("HELP");
                        break;
                    default:
                        Console.WriteLine("Unknown command. Type \"help\" for command list.");
                        break;
                }
            }

            // Cleanup
            if (indexer.Working)
                indexer.Stop();
            importer.RemoveAll();
        }

        private static void UpdateDrives(object sender, ElapsedEventArgs e) {
            driveList = new();
            foreach (string driveName in Environment.GetLogicalDrives()) {
                DriveInfo drive = new(driveName);
                //if (drive.DriveType == DriveType.Removable)
                    driveList.Add(drive);
            }
        }
    }
}
