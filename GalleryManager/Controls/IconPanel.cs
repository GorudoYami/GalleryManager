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
                iconPictureBox.Click += value;
            }
            remove {
                base.Click -= value;
                iconPictureBox.Click -= value;
            }
        }

        public new event EventHandler DoubleClick {
            add {
                base.DoubleClick += value;
                iconPictureBox.DoubleClick += value;
            }
            remove {
                base.DoubleClick -= value;
                iconPictureBox.DoubleClick -= value;
            }
        }

        public new event EventHandler MouseEnter {
            add {
                base.MouseEnter += value;
                iconPictureBox.MouseEnter += value;
            }
            remove {
                base.MouseEnter -= value;
                iconPictureBox.MouseEnter -= value;
            }
        }

        public new event EventHandler MouseLeave {
            add {
                base.MouseLeave += value;
                iconPictureBox.MouseLeave += value;
            }
            remove {
                base.MouseLeave -= value;
                iconPictureBox.MouseLeave -= value;
            }
        }

        public IconPanel() {
            InitializeComponent();
            iconPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
