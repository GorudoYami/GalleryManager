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

            storage = new DatabaseStorage(connectionInfo, galleryPath, menu);
            if (storage.Setup())
                menu.ShowMessage("Storage setup successful!");
            else {
                menu.ShowMessage("You broke something and storage setup failed.");
                Console.ReadKey();
                return;
            }

            // Setup indexer
            indexer = new Indexer(storage, galleryPath, menu);
            menu.ShowMessage("Indexer initialized.");

            // Setup importer
            importer = new Importer(storage, galleryPath);
            menu.ShowMessage("Importer initialized.");
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
