using System;
using System.Windows.Forms;

namespace DWinverNS
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[]args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ArgsMgr.debugMode = false;
            foreach (string arg in args)
            {
                if (arg.Equals("-debug", StringComparison.OrdinalIgnoreCase))
                {
                    ArgsMgr.debugMode = true;
                }
            }

            if (ArgsMgr.debugMode)
            {
                MessageBox.Show("Debug mode eneabled!");
                Application.Run(new CaD()); //ONLY CALL IF DEFAULT FORM WINDOW IS NEEDED!
            }
        }
    }
}