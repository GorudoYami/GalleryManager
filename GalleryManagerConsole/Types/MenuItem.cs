using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryManagerConsole.Types {
    public class MenuItem {
        public string Text { get; set; }
        public MenuItem(string text) {
            Text = text;
        }
    }
}
