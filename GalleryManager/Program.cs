using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace GalleryManager {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Debug.WriteLine("Starting the application");

            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.Run(new MainWindow());
        }
    }
}
