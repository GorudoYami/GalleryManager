using GalleryManager.Models;
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

namespace GalleryManager.Controls {
    public partial class CollectionTab : UserControl {
        public CollectionTab() {
            InitializeComponent();
            collectionView.BaseDirectory = new DirectoryInfo(Properties.Settings.Default.GalleryPath);
            collectionView.Reload();
        }
    }
}
