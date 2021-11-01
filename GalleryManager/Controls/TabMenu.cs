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
    public partial class TabMenu : UserControl {
        public int SelectedIndex { get; set; }
        public List<UserControl> Tabs { get; set; }

        private bool extended;
        public TabMenu() {
            InitializeComponent();

            extended = false;
            mainLayout.ColumnStyles[0].Width = 60F;

            SelectedIndex = 0;
            MenuOption option = (MenuOption)mainLayout.GetControlFromPosition(0, SelectedIndex + 1);
            option.BackColor = Color.FromArgb(100, 100, 100);

            Tabs = new List<UserControl> {
                new CollectionTab(),
                new ImportTab(),
                new DuplicatesTab(),
                new OptionsTab(),
                new InfoTab()
            };

            foreach (UserControl control in Tabs) {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0);
                control.Padding = new Padding(0);
            }

            mainLayout.Controls.Add(Tabs[SelectedIndex], 1, 0);
            mainLayout.SetRowSpan(Tabs[SelectedIndex], 7);
        }

        private void MenuIcon_Click(object sender, EventArgs e) {
            if (extended)
                mainLayout.ColumnStyles[0].Width = 60F;
            else
                mainLayout.ColumnStyles[0].Width = 160F;
            extended = !extended;
        }

        private void MenuOption_Click(object sender, EventArgs e) {

        }

        private void MenuOption_MouseEnter(object sender, EventArgs e) {

        }

        private void MenuOption_MouseLeave(object sender, EventArgs e) {

        }
    }
}
