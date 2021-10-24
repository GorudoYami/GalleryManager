using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalleryManager.Controls {
    public partial class MenuOption : UserControl {
        public Image Image {
            get {
                return iconPanel.Image;
            }
            set {
                iconPanel.Image = value;
            }
        }

        public string OptionText {
            get {
                return label.Text;
            }
            set {
                label.Text = value;
            }
        }

        public int Index { get; set; }

        public MenuOption() {
            InitializeComponent();
        }
    }
}
