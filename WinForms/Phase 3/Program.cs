using System;
using System.Windows.Forms;

namespace Interface
{
    public static class Program
    {
        public static Project p;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static void Initialize()
        {
            p = new Project();
            p.Initialize("input.txt");
        }

        public static void Serialize(string filename)
        {
            p.Serialize(filename);
        }

        public static void Deserialize(string filename)
        {
            p = p.Deserialize(filename);
        }
    }
}