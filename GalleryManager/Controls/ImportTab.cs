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

using GalleryManager.Types;

namespace GalleryManager.Controls {
    public partial class ImportTab : UserControl {

        public ImportTab() {
            InitializeComponent();

            // Changing the header background and foreground color
            listView.DrawColumnHeader += (sender, e) => {
                using SolidBrush bgBrush = new(listView.BackColor);
                using SolidBrush fgBrush = new(listView.ForeColor);
                e.Graphics.FillRectangle(bgBrush, e.Bounds);

                // Margins for the text
                Rectangle bounds = e.Bounds;
                bounds.Offset(5, 5);
                bounds.Width -= 5;
                bounds.Height -= 5;

                e.Graphics.DrawString(e.Header.Text, e.Font, fgBrush, bounds);
            };
            //listView.DrawColumnHeader += (sender, e) => e.DrawDefault = true;
            listView.DrawSubItem += (sender, e) => e.DrawDefault = true;
            listView.DrawItem += (sender, e) => e.DrawDefault = true;

            LoadDrives();
        }

        private void LoadDrives() {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (var drive in drives) {
                // TODO
                // Add an async "ready check" for drives (that are not active) with timeout just in case
                ListViewItem item = new() {
                    Text = drive.Name
                };
                item.SubItems.Add(new ListViewItem.ListViewSubItem() {
                    Text = drive.VolumeLabel
                });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() {
                    Text = Units.BytesToHumanReadable(drive.TotalSize)
                });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() {
                    Text = Units.BytesToHumanReadable(drive.TotalSize - drive.AvailableFreeSpace)
                });
                item.SubItems.Add(new ListViewItem.ListViewSubItem() {
                    Text = "Ready"
                });

                listView.Items.Add(item);
            }
        }
    }
}
