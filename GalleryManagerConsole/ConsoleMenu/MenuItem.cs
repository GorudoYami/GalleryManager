using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryManagerConsole.ConsoleMenu {
    public class MenuItem {
        public string Title { get; set; }
        public string DisplayTitle { get; set; }
        public bool Input { get; set; }
        public bool Exit { get; set; }
        public string Text { get; set; }
        public event EventHandler ItemSelected;

        public MenuItem(string title) {
            Title = title;
            DisplayTitle = string.Empty;
            Input = false;
            Text = string.Empty;
            Exit = false;
        }

        public void OnItemSelected(EventArgs e) {
            EventHandler handler = ItemSelected;
            handler?.Invoke(this, e);
        }
    }
}
