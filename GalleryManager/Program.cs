using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace GalleryManager {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Debug.Listeners.Add(new TextWriterTraceListener("debug.log"));
            Debug.AutoFlush = true;
            Debug.WriteLine("Starting the application");

            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            Application.Run(new MainWindow());
        }
    }
}
