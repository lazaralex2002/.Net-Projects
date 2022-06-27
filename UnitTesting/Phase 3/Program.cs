using System;
using System.Windows.Forms;


namespace LazarAlexandruConstantin
{
    public static class Program
    {
        public static Project project;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form());
        }
    }
}