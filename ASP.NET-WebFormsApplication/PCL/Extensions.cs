using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
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
        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        public static string SerializeToString(this Project project)
        {
            string result = "";
            XmlSerializer x = new XmlSerializer(typeof(Project));
            TextWriter writer = new Utf8StringWriter();
            x.Serialize(writer, project);
            result = writer.ToString();
            writer.Close();
            return result;
        }

        public static Project Deserialize(this Project _, string filename)
        {
            var mySerializer = new XmlSerializer(typeof(Project));
            var myFileStream = new FileStream(filename, FileMode.Open);
            Project project = (Project)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();
            return project;
        }

        public static Project DeserializeFromString(this Project _, string text)
        {
            var mySerializer = new XmlSerializer(typeof(Project));
            byte[] byteArray = Encoding.ASCII.GetBytes(text);
            MemoryStream stream = new MemoryStream(byteArray);
            Project project = (Project)mySerializer.Deserialize(stream);
            stream.Close();
            return project;
        }

        public static string ContentToString(this HashSet<string> set)
        {
            string message = "";
            if (set != null)
            {
                foreach (var resouce in set)
                {
                    message += resouce + " ";
                }
                return message;
            }
            else return "";
        }

        public static string ContentToString(this HashSet<Task> set)
        {
            if (set != null)
            {
                string message = "";
                foreach (var task in set)
                {
                    message += task.TaskID + " ";
                }
                return message;
            }
            else return ""; 
            
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

        public static bool Validate(int colIndex, string value)
        {
            switch (colIndex)
            {
                case Columns.TaskID:
                    if (value.IsInteger())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Columns.TaskName:
                    return true;
                case Columns.Duration:
                    if (value.IsInteger())
                    {
                        try
                        {
                            var number = int.Parse(value);
                        }
                        catch
                        {
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Columns.Finish:
                    try
                    {
                         DateTime.ParseExact(value.Trim(), "M/dd/yyyy hh:mm:ss tt", null);
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                case Columns.Start:
                    try
                    {
                        DateTime.ParseExact(value.Trim(), "M/dd/yyyy hh:mm:ss tt", null);
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                default:
                    return false;
            }
        }
    }
}
