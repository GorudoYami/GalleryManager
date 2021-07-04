using GalleryManager.Storage;
using GalleryManager.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalleryManager {
    public partial class DuplicatesDialog : System.Windows.Forms.Form {
        private List<Media> videos;
        private List<Media> pictures;
        private IStorage storage;

        public DuplicatesDialog(ref IStorage storage) {
            InitializeComponent();
            this.storage = storage;
        }

        private void buttonSearch_Click(object sender, EventArgs e) {
            Search();
        }

        private void buttonDelete_Click(object sender, EventArgs e) {
            List<Media> delete = new List<Media>();
            foreach (ListViewItem item in listView.CheckedItems) {
                bool found = false;
                foreach (Media media in pictures) {
                    if (media.Path == item.SubItems[1].Text) {
                        found = true;
                        delete.Add(media);
                        break;
                    }
                }

                if (!found) {
                    foreach (Media media in videos) {
                        if (media.Path == item.SubItems[1].Text) {
                            found = true;
                            delete.Add(media);
                            break;
                        }
                    }
                }
            }

            if (storage.Delete(delete))
                MessageBox.Show("Successfully deleted selected media.", "Success");
            else
                MessageBox.Show("Couldn't delete media.", "Error");
            Search();
        }

        private void Search() {
            listView.Items.Clear();
            if (radioHashes.Checked) {
                videos = storage.GetDuplicates(Media.Type.VIDEO);
                pictures = storage.GetDuplicates(Media.Type.PICTURE);
            }
            else {
                videos = new List<Media>();
                pictures = new List<Media>();
            }

            bool color = false;
            string hash = string.Empty;
            foreach (Media video in videos) {
                ListViewItem item = new ListViewItem(video.Hash);

                if (hash == video.Hash) {
                    if (color)
                        item.BackColor = Color.LightGreen;
                }
                else {
                    hash = video.Hash;
                    color = !color;
                    if (color)
                        item.BackColor = Color.LightGreen;
                }

                item.SubItems.Add(video.Path);
                listView.Items.Add(item);
            }

            foreach (Media picture in pictures) {
                ListViewItem item = new ListViewItem(picture.Hash);

                if (hash == picture.Hash) {
                    if (color)
                        item.BackColor = Color.LightGreen;
                }
                else {
                    hash = picture.Hash;
                    color = !color;
                    if (color)
                        item.BackColor = Color.LightGreen;
                }

                item.SubItems.Add(picture.Path);
                listView.Items.Add(item);
            }
        }
    }
}
