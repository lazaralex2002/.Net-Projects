using System;
using System.Diagnostics;
using System.IO;

namespace Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            Project p = new Project();
            p.Initialize("input.txt");
            Debug.WriteLine(p);
        }
    }
}
