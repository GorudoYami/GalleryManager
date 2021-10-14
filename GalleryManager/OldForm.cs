using GalleryManager.Storage;
using GalleryManager.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace GalleryManager {
    public partial class OldForm : System.Windows.Forms.Form {
        private readonly Indexer indexer;
        private readonly Importer importer;
        private IStorage storage;
        private Progress<double> indexerProgress;
        private Dictionary<string, Progress<int>> importerProgress;

        public OldForm() {
            Debug.WriteLine("Initializing components");
            InitializeComponent();
            Debug.Indent();
            Debug.WriteLine("Done");
            Debug.Unindent();

            Debug.WriteLine("Loading options");
            Debug.Indent();
            LoadOptions();
            Debug.WriteLine("Done");
            Debug.Unindent();

            Debug.WriteLine("Loading gallery");
            Debug.Indent();
            LoadGallery();
            Debug.WriteLine("Done");
            Debug.Unindent();

            Debug.WriteLine("Loading info for database");
            Debug.Indent();
            Indexer.Mode mode;
            // Set operating mode
            if (radioButtonMySQL.Checked) {
                mode = Indexer.Mode.MYSQL;

                // Load connection info for MySQL
                string[] host = Properties.Settings.Default.Host.Split(':');
                if (host.Length != 2) {
                    MessageBox.Show("Invalid database host - using defaults.", "Warning");
                    Properties.Settings.Default.Host = "localhost:3306";
                    host = Properties.Settings.Default.Host.Split(':');
                }

                ConnectionInfo info = new ConnectionInfo {
                    Server = host[0],
                    Port = host[1],
                    User = Properties.Settings.Default.User,
                    Password = Properties.Settings.Default.Password,
                    Database = Properties.Settings.Default.Database
                };
                storage = new MySqlStorage(info);
            }
            else if (radioButtonSQLite.Checked) {
                mode = Indexer.Mode.SQLITE;
                storage = new SQLiteStorage();
            }
            else if (radioButtonJSON.Checked) {
                mode = Indexer.Mode.JSON;
                storage = new JsonStorage();
            }
            else {
                mode = Indexer.Mode.MYSQL;
            }
            Debug.WriteLine("Done");
            Debug.Unindent();

            // Begin storage setup
            Debug.WriteLine("Beginning storage setup");
            Debug.Indent();
            if (!storage.Setup())
                MessageBox.Show("Storage setup failed!", "Error");
            Debug.WriteLine("Done");
            Debug.Unindent();

            // Indexer setup
            Debug.WriteLine("Beginning indexer setup");
            Debug.Indent();
            indexerProgress = new Progress<double>(value => labelPercent.Text = value.ToString() + "%");
            indexer = new Indexer(mode, storage, Properties.Settings.Default.GalleryPath, indexerProgress);
            if (radioButtonAutomatic.Checked)
                indexer.Start();
            Debug.WriteLine("Done");
            Debug.Unindent();

            // Importer setup
            Debug.WriteLine("Beginning importer setup");
            Debug.Indent();
            importerProgress = new Dictionary<string, Progress<int>>();
            importerProgress["newVideos"] = new Progress<int>(value => labelNewVideos.Text = value.ToString());
            importerProgress["newPictures"] = new Progress<int>(value => labelNewPictures.Text = value.ToString());
            importerProgress["pictureCount"] = new Progress<int>(value => labelFoundPictures.Text = value.ToString());
            importerProgress["videoCount"] = new Progress<int>(value => labelFoundVideos.Text = value.ToString());
            importerProgress["overall"] = new Progress<int>(value => progressBarImport.Value = value);
            importer = new Importer(storage, indexer, Properties.Settings.Default.GalleryPath, importerProgress);
            Debug.WriteLine("Done");
            Debug.Unindent();
        }

        private void LoadOptions() {
            // Load from properties to text boxes
            textBoxHost.Text = Properties.Settings.Default.Host;
            Debug.WriteLine("Host: " + textBoxHost.Text);
            textBoxUser.Text = Properties.Settings.Default.User;
            Debug.WriteLine("User: " + textBoxUser.Text);
            textBoxPassword.Text = Properties.Settings.Default.Password;
            Debug.WriteLine("Password: " + textBoxPassword.Text);
            textBoxDatabase.Text = Properties.Settings.Default.Database;
            Debug.WriteLine("Database: " + textBoxDatabase.Text);
            textBoxPath.Text = Properties.Settings.Default.GalleryPath;
            Debug.WriteLine("GalleryPath: " + textBoxPath.Text);

            if (Properties.Settings.Default.Startup == 0)
                radioButtonAutomatic.Checked = true;
            else if (Properties.Settings.Default.Startup == 1)
                radioButtonManual.Checked = true;
            Debug.WriteLine("Startup: " + Properties.Settings.Default.Startup);

            if (Properties.Settings.Default.Storage == 0)
                radioButtonMySQL.Checked = true;
            else if (Properties.Settings.Default.Storage == 1)
                radioButtonSQLite.Checked = true;
            else if (Properties.Settings.Default.Storage == 2)
                radioButtonJSON.Checked = true;
            Debug.WriteLine("Storage: " + Properties.Settings.Default.Storage);
        }

        private async void SaveOptions() {
            Properties.Settings.Default.Host = textBoxHost.Text;
            Debug.WriteLine("Host: " + textBoxHost.Text);
            Properties.Settings.Default.User = textBoxUser.Text;
            Debug.WriteLine("User: " + textBoxUser.Text);
            Properties.Settings.Default.Password = textBoxPassword.Text;
            Debug.WriteLine("Password: " + textBoxPassword.Text);
            Properties.Settings.Default.Database = textBoxDatabase.Text;
            Debug.WriteLine("Database: " + textBoxDatabase.Text);
            Properties.Settings.Default.GalleryPath = textBoxPath.Text;
            Debug.WriteLine("GalleryPath: " + textBoxPath.Text);

            if (radioButtonAutomatic.Checked)
                Properties.Settings.Default.Startup = 0;
            else
                Properties.Settings.Default.Startup = 1;
            Debug.WriteLine("Startup: " + Properties.Settings.Default.Startup);

            if (radioButtonMySQL.Checked)
                Properties.Settings.Default.Storage = 0;
            else if (radioButtonSQLite.Checked)
                Properties.Settings.Default.Storage = 1;
            else if (radioButtonJSON.Checked)
                Properties.Settings.Default.Storage = 2;
            Debug.WriteLine("Storage: " + Properties.Settings.Default.Storage);

            Properties.Settings.Default.Save();
            await indexer.UpdateGalleryPath(Properties.Settings.Default.GalleryPath);
        }

        private void AddDirectoryNodes(DirectoryInfo directory, TreeNode parent) {
            // Add subdirectories
            foreach (DirectoryInfo subdir in directory.GetDirectories())
                AddDirectoryNodes(subdir, parent.Nodes.Add(subdir.Name));

            // Add files
            foreach (FileInfo file in directory.GetFiles()) {
                if (IsVideo(file) || IsPicture(file))
                    parent.Nodes.Add(file.Name);
            }
        }

        private void LoadGallery() {
            treeView.CheckBoxes = true;

            DirectoryInfo rootDirectory = new DirectoryInfo(Properties.Settings.Default.GalleryPath);
            TreeNode rootNode = treeView.Nodes["nodeMedia"];

            AddDirectoryNodes(rootDirectory, rootNode);
        }

        private void LoadImports() {
            if (listView.Items.Count > 0) {
                ListViewItem[] items = new ListViewItem[listView.Items.Count];
                listView.Items.CopyTo(items, 0);
                listView.Items.Clear();
                foreach (string driveName in Environment.GetLogicalDrives()) {
                    DriveInfo drive = new DriveInfo(driveName);
                    ListViewItem item = new ListViewItem {
                        Tag = drive
                    };
                    item.SubItems.Add(drive.Name);
                    item.SubItems.Add(drive.VolumeLabel);
                    item.SubItems.Add(Convert.ToString(drive.TotalSize / 1000000000) + " GB");
                    item.SubItems.Add((drive.TotalSize - drive.AvailableFreeSpace) / 1000000000 + " GB (" + ((drive.TotalSize - drive.AvailableFreeSpace) / drive.TotalSize * 100) + "%)");

                    // Sets previous check state
                    for (int i = 0; i < items.Length; i++) {
                        if (items[i].SubItems[0].Text == item.SubItems[0].Text) {
                            items[i].Checked = item.Checked;
                            break;
                        }
                    }

                    listView.Items.Add(item);
                }

                // Load added directories
                foreach (ListViewItem item in items) {
                    if (item.Tag is DirectoryInfo) {
                        listView.Items.Add(item);

                        // Sets previous check state
                        for (int i = 0; i < items.Length; i++) {
                            if (items[i].SubItems[0].Text == item.SubItems[0].Text) {
                                items[i].Checked = item.Checked;
                                break;
                            }
                        }
                    }
                }
            }
            else {
                // Iterate through available drives and add them
                foreach (string driveName in Environment.GetLogicalDrives()) {
                    DriveInfo drive = new DriveInfo(driveName);
                    ListViewItem item = new ListViewItem {
                        Tag = drive
                    };
                    item.SubItems.Add(drive.Name);
                    item.SubItems.Add(drive.VolumeLabel);
                    item.SubItems.Add(Convert.ToString(drive.TotalSize / 1000000000) + " GB");
                    item.SubItems.Add((drive.TotalSize - drive.AvailableFreeSpace) / 1000000000 + " GB (" + ((drive.TotalSize - drive.AvailableFreeSpace) / drive.TotalSize * 100) + "%)");
                    listView.Items.Add(item);
                }
            }
        }

        private async void LoadStats() {
            string result = await Task.Run(() => storage.TotalPictures().ToString());
            labelTotalPictures.Text = result;
            result = await Task.Run(() => storage.TotalVideos().ToString());
            labelTotalVideos.Text = result;
            labelTotalUnknown.Text = "Not implemented";
            labelTotalAlbums.Text = "Not implemented";
            labelTotalDirs.Text = "Not implemented";
            if (indexer.Working) {
                labelStatus.Text = "working";
                labelStatus.ForeColor = Color.Green;
            }
            else {
                labelStatus.Text = "stopped";
                labelStatus.ForeColor = Color.Red;
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e) {
            if (tabControl.SelectedIndex == 1)
                LoadImports();
            else if (tabControl.SelectedIndex == 2)
                LoadStats();
            else if (tabControl.SelectedIndex == 4)
                LoadOptions();
        }

        private void OptionsChanged(object sender, EventArgs e) {
            if (!radioButtonMySQL.Checked) {
                textBoxHost.Enabled = false;
                textBoxUser.Enabled = false;
                textBoxPassword.Enabled = false;
                textBoxDatabase.Enabled = false;
                buttonTest.Enabled = false;
            }
            else {
                textBoxHost.Enabled = true;
                textBoxUser.Enabled = true;
                textBoxPassword.Enabled = true;
                textBoxDatabase.Enabled = true;
                buttonTest.Enabled = true;
            }
        }

        private async void buttonToggle_Click(object sender, EventArgs e) {
            if (indexer.Working)
                await indexer.Stop();
            else
                indexer.Start();
        }

        private void buttonTest_Click(object sender, EventArgs e) {
            SaveOptions();
            string[] host = Properties.Settings.Default.Host.Split(':');
            if (host.Length != 2) {
                MessageBox.Show("Invalid database host - using defaults.", "Warning");
                Properties.Settings.Default.Host = "localhost:3306";
                host = Properties.Settings.Default.Host.Split(':');
            }

            ConnectionInfo info = new ConnectionInfo {
                Server = host[0],
                Port = host[1],
                User = Properties.Settings.Default.User,
                Password = Properties.Settings.Default.Password,
                Database = Properties.Settings.Default.Database
            };
            ((MySqlStorage)(storage)).CreateConnectionString(info);

            if (!storage.Setup())
                MessageBox.Show("Storage setup failed!", "Error");
            else
                MessageBox.Show("Everything's okay!", "Success");
        }

        private void CheckChildNodes(TreeNode parent, bool value) {
            foreach (TreeNode child in parent.Nodes)
                CheckChildNodes(child, value);
            parent.Checked = value;
        }

        private void treeView_AfterCheck(object sender, TreeViewEventArgs e) {
            if (e.Action != TreeViewAction.Unknown)
                CheckChildNodes(e.Node, e.Node.Checked);
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            SaveOptions();
        }

        private async void listView_ItemChecked(object sender, ItemCheckedEventArgs e) {
            if (e.Item.Checked) {
                if (e.Item.Tag is DriveInfo)
                    importer.AddDrive((DriveInfo)e.Item.Tag);
                else if (e.Item.Tag is DirectoryInfo)
                    importer.AddDirectory((DirectoryInfo)e.Item.Tag);
                labelProgress.Text = "Analyzing...";
            }
            else if (e.Item != null && e.Item.Tag != null && e.Item.Focused) {
                if (e.Item.Tag is DriveInfo)
                    await importer.RemoveDrive((DriveInfo)e.Item.Tag);
                else if (e.Item.Tag is DirectoryInfo)
                    await importer.RemoveDirectory((DirectoryInfo)e.Item.Tag);
            }
        }

        private void buttonAddDirectory_Click(object sender, EventArgs e) {;
            using FolderBrowserDialog dialog = new FolderBrowserDialog {
                Description = "Select directory you want to import",
                ShowNewFolderButton = false,
                RootFolder = Environment.SpecialFolder.MyComputer
            };
            if (dialog.ShowDialog() == DialogResult.OK) {
                DirectoryInfo directory = new DirectoryInfo(dialog.SelectedPath);

                ListViewItem item = new ListViewItem {
                    Tag = directory
                };
                item.SubItems.Add(directory.FullName);
                item.SubItems.Add(directory.Name);
                item.SubItems.Add("N/A");
                item.SubItems.Add("N/A");
                listView.Items.Add(item);
            }
        }

        private async void buttonImport_Click(object sender, EventArgs e) {
            // Check if importer finished analyzing
            if (await importer.ImportReadyAsync()) {
                importer.Import();
                labelProgress.Text = "Importing...";
            }
            else
                MessageBox.Show("Imports are not ready yet", "Warning");
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e) {
            string path = Properties.Settings.Default.GalleryPath + e.Node.FullPath.Replace("Media\\", string.Empty);
            // Display a preview of media selected
            // It displays a full image, wonder if it can be changed to smaller thumbnail
            if (!IsVideo(new FileInfo(path)))
                pictureBoxPreview.ImageLocation = path;
            else
                pictureBoxPreview.Image = null;
        }

        private bool IsVideo(FileInfo file) {
            switch (file.Extension.ToUpper()) {
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

        private bool IsPicture(FileInfo file) {
            switch (file.Extension.ToUpper()) {
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
                case ".NEF":
                    return true;
                default:
                    return false;
            }
        }
    }
}
