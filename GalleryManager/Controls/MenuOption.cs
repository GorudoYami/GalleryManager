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
            get { return iconPanel.Image; }
            set { iconPanel.Image = value; }
        }

        public string OptionText {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public int Index { get; set; }

        public new event EventHandler Click {
            add {
                base.Click += value;
                foreach (Control control in mainLayout.Controls) {
                    control.Click += value;
                }
            }
            remove {
                base.Click -= value;
                foreach (Control control in mainLayout.Controls) {
                    control.Click -= value;
                }
            }
        }

        public new event EventHandler DoubleClick {
            add {
                base.DoubleClick += value;
                foreach (Control control in mainLayout.Controls) {
                    control.DoubleClick += value;
                }
            }
            remove {
                base.DoubleClick -= value;
                foreach (Control control in mainLayout.Controls) {
                    control.DoubleClick -= value;
                }
            }
        }

        public new event EventHandler MouseEnter {
            add {
                base.MouseEnter += value;
                foreach (Control control in mainLayout.Controls) {
                    control.MouseEnter += value;
                }
            }
            remove {
                base.MouseEnter -= value;
                foreach (Control control in mainLayout.Controls) {
                    control.MouseEnter -= value;
                }
            }
        }

        public new event EventHandler MouseLeave {
            add {
                base.MouseLeave += value;
                foreach (Control control in mainLayout.Controls) {
                    control.MouseLeave += value;
                }
            }
            remove {
                base.MouseLeave -= value;
                foreach (Control control in mainLayout.Controls) {
                    control.MouseLeave -= value;
                }
            }
        }

        public MenuOption() {
            InitializeComponent();
        }
    }
}
