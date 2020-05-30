using System;
using System.Windows.Forms;

namespace NetBDI.Example2GUI
{
    /// <summary>
    /// The main program
    /// </summary>
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GoldMineGUI());
        }
    }
}
