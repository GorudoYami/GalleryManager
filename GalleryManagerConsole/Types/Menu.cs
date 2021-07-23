using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryManagerConsole.Types {
    public class Menu {
        public string Title { get; set; }
        public Dictionary<MenuItem, Menu> SubMenu { get; set; }
        public List<MenuItem> Items { get; set; }
        public Menu(string title, List<MenuItem> items = null) {
            Title = title;
            if (items != null)
                Items = items;
            else
                Items = new List<MenuItem>();
        }

        public void Show() {
            throw new NotImplementedException();
        }
    }
}
