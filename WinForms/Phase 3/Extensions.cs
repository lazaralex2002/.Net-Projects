using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Interface
{
    static internal class Extensions
    {
        public static void Serialize(this Project p, string filename)
        {
            XmlSerializer x = new XmlSerializer(typeof(Project));
            TextWriter writer = new StreamWriter(filename);
            x.Serialize(writer, p);
            writer.Close();
        }

        public static Project Deserialize(this Project x, string filename)
        {
            Project p = new Project();
            var mySerializer = new XmlSerializer(typeof(Project));
            var myFileStream = new FileStream(filename, FileMode.Open);
            p = (Project)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();
            return p;
        }

        public static string ContentToString(this HashSet<string> set)
        {
            string message = "";
            foreach (var resouce in set)
            {
                message += resouce + " ";
            }
            return message;
        }

        public static string ContentToString(this HashSet<Task> set)
        {
            string message = "";
            foreach (var task in set)
            {
                message += task.TaskID + " ";
            }
            return message;
        }

        public static bool IsInteger(this string s)
        {
            int n = s.Length;
            for (int i = 0; i < n; ++i)
            {
                if (s[i] < '0' || s[i] > '9') return false;
            }
            return true;
        }
    }
}
