using GalleryManager.Controls;
using GalleryManager.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GalleryManager {
    public partial class MainWindow : Form {
        // Win32 API stuff so dragging window works
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private TabMenu tabMenu;

        public MainWindow() {
            InitializeComponent();

            tabMenu = new TabMenu();
            tabMenu.Dock = DockStyle.Fill;
            tabMenu.Margin = new Padding(0);
            tabMenu.Padding = new Padding(0);
            mainLayout.Controls.Add(tabMenu, 0, 1);
        }

        // Win32 API magic to allow dragging by titlebar
        private void Navbar_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void IconMinimize_Click(object sender, EventArgs e) {
            WindowState = FormWindowState.Minimized;
        }

        private void IconMaximize_Click(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void IconClose_Click(object sender, EventArgs e) =>
            Close();

        //private void Menu_MouseEnter(object sender, EventArgs e) {
        //    Control control = (Control)sender;
        //    foreach (Control c in menuList[Convert.ToInt32(control.Tag)])
        //        c.BackColor = Color.FromArgb(100, 100, 100);
        //}

        //private void Menu_MouseLeave(object sender, EventArgs e) {
        //    Control control = (Control)sender;
        //    int index = Convert.ToInt32(control.Tag);

        //    // If it's an active menu item -> don't reset the color
        //    if (index == activeMenuIndex)
        //        return;

        //    foreach (Control c in menuList[index])
        //        c.BackColor = Color.FromArgb(55, 55, 55);
        //}

        //private void Menu_Click(object sender, EventArgs e) {
        //    Control control = (Control)sender;
        //    int index = Convert.ToInt32(control.Tag);
        //    if (index == activeMenuIndex)
        //        return;

        //    // Reset previous menu item
        //    foreach (Control c in menuList[activeMenuIndex])
        //        c.BackColor = Color.FromArgb(55, 55, 55);

        //    // Highlight selected menu item
        //    activeMenuIndex = index;
        //    foreach (Control c in menuList[activeMenuIndex])
        //        c.BackColor = Color.FromArgb(100, 100, 100);
        //}

        //private void MenuIcon_Click(object sender, EventArgs e) {
        //    //if (extendedMenu) {
        //    //    contentLayout.ColumnStyles[0].Width = 65F;
        //    //    menuIcon.Image = Properties.Resources.menu;
        //    //}
        //    //else {
        //    //    contentLayout.ColumnStyles[0].Width = 200F;
        //    //    menuIcon.Image = Properties.Resources.menu_alt_2;
        //    //}

        //    //extendedMenu = !extendedMenu;
        //}

        private void ButtonLabel_MouseEnter(object sender, EventArgs e) {
            Label label = (Label)sender;
            label.BackColor = Color.FromArgb(100, 100, 100);
        }

        private void ButtonLabel_MouseLeave(object sender, EventArgs e) {
            Label label = (Label)sender;
            label.BackColor = Color.FromArgb(55, 55, 55);
        }
    }

}
