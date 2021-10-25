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
    public partial class IconPanel : UserControl {
        public Image Image {
            get { return iconPictureBox.Image; }
            set { iconPictureBox.Image = value; }
        }

        public new event EventHandler Click {
            add {
                base.Click += value;
                foreach (Control control in Controls) {
                    control.Click += value;
                }
            }
            remove {
                base.Click -= value;
                foreach (Control control in Controls) {
                    control.Click -= value;
                }
            }
        }

        public new event EventHandler DoubleClick {
            add {
                base.DoubleClick += value;
                foreach (Control control in Controls) {
                    control.DoubleClick += value;
                }
            }
            remove {
                base.DoubleClick -= value;
                foreach (Control control in Controls) {
                    control.DoubleClick -= value;
                }
            }
        }

        public IconPanel() {
            InitializeComponent();
            iconPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
