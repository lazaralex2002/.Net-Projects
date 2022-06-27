using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace LazarAlexandruConstantin
{
    public static class Extensions
    {
        public static void Serialize(this Project project, string filename)
        {
            XmlSerializer x = new XmlSerializer(typeof(Project));
            TextWriter writer = new StreamWriter(filename);
            x.Serialize(writer, project);
            writer.Close();
        }

        public static Project Deserialize(this Project _, string filename)
        {

            try
            {
                var mySerializer = new XmlSerializer(typeof(Project));
                var myFileStream = new FileStream(filename, FileMode.Open);
                Project project = (Project)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return project;
            }
            catch(Exception e)
            {
                MessageBox.Show($"Error: {e.GetType()}");
            }
            return null;
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
            int n = s.Length; int i = 0;
            if (s == null || n == 0) return false;
            
            if ( s[0] == '-')
            {
                if (s[1] == '0')
                {
                    return false;
                }
                i++;
            }

            if ( s[0] == '0' && n > 1)
            {
                return false;
            }

            for ( ; i < n; ++i)
            {
                if (s[i] < '0' || s[i] > '9') return false;
            }
            return true;
        }

        public static Task GetTask(this HashSet<Task> taskSet, int taskID)
        {
            foreach (Task task in taskSet)
            {
                if (task.TaskID == taskID) return task;
            }
            return null;
        }
    }
}
