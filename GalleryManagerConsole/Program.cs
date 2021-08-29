using System.Configuration;
using System.Collections.Specialized;
using System;
using System.Timers;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

using GalleryManagerConsole.Types;
using GalleryManagerConsole.Storage;
using GalleryManagerConsole.ConsoleMenu;
using System.Threading;

namespace GalleryManagerConsole {
    public class Program {

        private static Indexer indexer;
        private static Importer importer;
        private static IStorage storage;
        private static string galleryPath;

        private static List<DriveInfo> driveList;
        private static Menu mainMenu;

        private static System.Timers.Timer driveTimer;
        public static void Main(string[] args) =>
            MainAsync(args).GetAwaiter().GetResult();

        public static async Task MainAsync(string[] args) {
            // Setup
            var menu = new Menu() {
                HeaderText = "Choose storage method",
                Choice = true
            };
            menu.Items.Add(new MenuItem("SQLite"));
            menu.Items.Add(new MenuItem("MySQL"));
            menu.Items.Add(new MenuItem("JSON"));
            menu.Show();

            var connectionInfo = new ConnectionInfo();
            if (menu.ActiveItemIndex == 0)
                connectionInfo.Type = ConnectionType.SQLite;
            else if (menu.ActiveItemIndex == 2)
                throw new NotImplementedException();
            else if (menu.ActiveItemIndex == 1) {
                menu = new Menu() {
                    HeaderText = "Enter MySQL connection info"
                };
                menu.Items.Add(new MenuItem("IP:") {
                    Input = true
                });
                menu.Items.Add(new MenuItem("Port:") {
                    Input = true
                });
                menu.Items.Add(new MenuItem("User:") {
                    Input = true
                });
                menu.Items.Add(new MenuItem("Password:") {
                    Input = true
                });
                menu.Items.Add(new MenuItem("Database:") {
                    Input = true
                });
                menu.Items.Add(new MenuItem("Done") {
                    Exit = true
                });
                menu.Show();
                connectionInfo.Type = ConnectionType.MySQL;
                connectionInfo.Server = menu.Items[0].Text;
                connectionInfo.Port = menu.Items[1].Text;
                connectionInfo.User = menu.Items[2].Text;
                connectionInfo.Password = menu.Items[3].Text;
                connectionInfo.Database = menu.Items[4].Text;
                connectionInfo.CreateConnectionString();
            }

            menu = new Menu() {
                HeaderText = "Enter gallery path"
            };
            menu.Items.Add(new MenuItem("Path:") {
                Input = true,
                Exit = true
            });
            menu.Show();
            galleryPath = menu.Items[0].Text.Replace("\\", "/");

            menu = new Menu() {
                HeaderText = "Save config for later?",
                Choice = true
            };
            menu.Items.Add(new MenuItem("Yes"));
            menu.Items.Add(new MenuItem("No"));
            menu.Show();

            if (menu.ActiveItemIndex == 0) {

            }
            else {

            }
               

            //if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("AskForConfig"))) {
            //    DrawMenu(0, 0);
            //}
            //else {
            //    DrawMenu()
            //}

            // Setup drive update timer
            driveTimer = new();
            driveTimer.Elapsed += UpdateDrives;
            driveTimer.Interval = 5000;
            UpdateDrives(null, null);

            storage = new DatabaseStorage(connectionInfo, galleryPath);
            if (storage.Setup())
                Console.WriteLine("Storage setup successful!");
            else {
                Console.WriteLine("You fucked up something and storage setup failed.");
                Console.ReadKey();
                return;
            }

            // Setup indexer
            indexer = new Indexer(storage, galleryPath);
            Console.WriteLine("Indexer initialized.");

            // Setup importer
            importer = new Importer(storage, galleryPath);
            Console.WriteLine("Importer initialized.");
            Thread.Sleep(1500);

            mainMenu = new Menu() {
                HeaderText = "Main Menu",
            };
            mainMenu.Items.Add(new MenuItem("Indexer"));
            mainMenu.Items[0].ItemSelected += IndexerSelected;
            mainMenu.Items.Add(new MenuItem("Importer"));
            mainMenu.Items[1].ItemSelected += ImporterSelected;
            mainMenu.Items.Add(new MenuItem("Exit") {
                Exit = true
            });

            mainMenu.Show();

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
                    case "CLEANUP":
                        Console.WriteLine("Starting cleanup process...");
                        if (!await storage.Cleanup())
                            Console.WriteLine("Cleanup failed!");
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
                        Console.WriteLine("CLEANUP");
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

        private static void ImporterSelected(object sender, EventArgs e) {
            var menu = new Menu() {
                HeaderText = "Importer Menu"
            };
            menu.Items.Add(new MenuItem("Add/remove drive"));
            menu.Items[0].ItemSelected += ImporterAddRemoveDriveSelected;
            menu.Items.Add(new MenuItem("Add/remove directory"));
            menu.Items[1].ItemSelected += ImporterAddRemoveDirectorySelected;
            menu.Items.Add(new MenuItem("Start importing"));
            menu.Items[2].ItemSelected += ImporterStartSelected;
            menu.Items.Add(new MenuItem("Return") {
                Exit = true
            });
            menu.Show();
            mainMenu.Update();
        }
        private static async void ImporterStartSelected(object sender, EventArgs e) {
            if (await importer.ImportReadyAsync())
                importer.StartImport();
        }
        private static void ImporterAddRemoveDriveSelected(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private static void ImporterAddRemoveDirectorySelected(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private static void IndexerSelected(object sender, EventArgs e) {
            var menu = new Menu() {
                HeaderText = "Indexer Menu"
            };
            string title;
            if (indexer.Working)
                title = "Stop indexer";
            else
                title = "Start indexer";

            menu.Items.Add(new MenuItem(title));
            menu.Items[0].ItemSelected += IndexerStartStopSelected;
            menu.Items.Add(new MenuItem("Cleanup"));
            menu.Items[1].ItemSelected += IndexerCleanupSelected;
            menu.Items.Add(new MenuItem("Return") {
                Exit = true
            });
            menu.Show();
            mainMenu.Update();
        }

        private static void IndexerStartStopSelected(object sender, EventArgs e) {
            Menu menu = (Menu)sender;
            if (!indexer.Working) {
                indexer.Start();
                menu.ActiveItem.Title = "Stop indexer";
            }
            else {
                indexer.Stop();
                menu.ActiveItem.Title = "Start indexer";
            }
        }

        private static async void IndexerCleanupSelected(object sender, EventArgs e) {
            await storage.Cleanup();
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
