using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalleryManager.Controls {
    public class IconPanel : Panel {
        private readonly PictureBox picture;
        public System.Drawing.Image Image {
            get { return picture.Image; }
            set { picture.Image = value; }
        }

        //public EventHandler MouseEnter { get; set; }

        public IconPanel() {
            picture = new PictureBox {
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                Name = "picture"
            };
            Controls.Add(picture);
        }
    }
}
