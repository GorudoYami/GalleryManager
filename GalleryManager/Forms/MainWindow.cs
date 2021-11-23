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
        private const int cGrip = 16;
        private const int cCaption = 32;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private readonly TabMenu tabMenu;

        public MainWindow() {
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);

            tabMenu = new TabMenu {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
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
