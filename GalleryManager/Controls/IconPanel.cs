using System;
using System.Diagnostics;
using System.Drawing;
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
                foreach (Control control in Controls)
                    control.Click += value;
            }
            remove {
                base.Click -= value;
                foreach (Control control in Controls)
                    control.Click -= value;
            }
        }

        public new event EventHandler DoubleClick {
            add {
                base.DoubleClick += value;
                foreach (Control control in Controls)
                    control.DoubleClick += value;
            }
            remove {
                base.DoubleClick -= value;
                foreach (Control control in Controls)
                    control.DoubleClick -= value;
            }
        }

        public new event EventHandler MouseEnter {
            add {
                base.MouseEnter += value;
                foreach (Control control in Controls)
                    control.MouseEnter += value;
            }
            remove {
                base.MouseEnter -= value;
                foreach (Control control in Controls)
                    control.MouseEnter -= value;
            }
        }

        public new event EventHandler MouseLeave {
            add {
                base.MouseLeave += value;
                foreach (Control control in Controls)
                    control.MouseLeave += value;
            }
            remove {
                base.MouseLeave -= value;
                foreach (Control control in Controls)
                    control.MouseLeave -= value;
            }
        }

        public IconPanel() {
            InitializeComponent();
            iconPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
