using System;
using System.Collections.Generic;
using System.IO;

namespace Interface
{
    public class Project
    {
        public HashSet<Task> tasks;

        public Project()
        {
            tasks = new HashSet<Task>();
        }

        public void Initialize(string fileName)
        {
            string[] lines = null;
            string fullName = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + '\\' + fileName;
            try
            {
                lines = System.IO.File.ReadAllLines(fullName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (lines != null)
            {
                AddTasks(lines);
            }
        }

        private void AddTasks(string[] lines)
        {
            foreach (var l in lines)
            {
                string line = l.Trim();
                if (line.StartsWith("SetTaskField"))
                {
                    line = line.Substring(line.IndexOf("SetTaskField") + "SetTaskField".Length);
                    line = line.Trim();
                    string[] fields = line.Split(',');
                    foreach (var field in fields)
                    {
                        string s = field;
                        s = s.Trim();
                    }
                    var properties = new Dictionary<string, string>();
                    int taskID = 0;
                    for (int i = 0; i < fields.Length; ++i)
                    {
                        if (fields[i].StartsWith("Field"))
                        {
                            string propertyName = fields[i].Substring(fields[i].IndexOf('=') + 1).Trim('"');
                            string value = fields[i + 1].Substring(fields[i].IndexOf('=') + 2).Trim('"');
                            properties.Add(propertyName, value);
                            i++;
                        }
                        else if (fields[i].Contains("TaskID"))
                        {
                            taskID = int.Parse(fields[i].Substring(fields[i].IndexOf('=') + 1).Trim('"'));
                        }
                    }
                    var currentTask = new Task(taskID);
                    if (!Exists(currentTask))
                    {
                        AddTaskProperties(currentTask, properties);
                        tasks.Add(currentTask);
                    }
                    else
                    {
                        foreach (var t in tasks)
                        {
                            if (t.Equals(currentTask))
                            {
                                AddTaskProperties(t, properties);
                            }
                        }
                    }
                }
            }
        }


        private bool Exists(Task currentTask)
        {
            if (tasks.Contains(currentTask))
            {
                return true;
            }
            return false;
        }
        private Task getTask(int index)
        {
            foreach ( var t in tasks )
            {
                if (t.TaskID == index) return t;
            }
            return null;
        }

        private void AddTaskProperties(Task currentTask, Dictionary<string, string> properties)
        {
            foreach (string key1 in properties.Keys)
            {
                string key = key1.Trim();
                switch (key)
                {
                    case "Name":
                        currentTask.Name = properties[key];
                        break;
                    case "Duration":
                        currentTask.Duration = Double.Parse(properties[key]);
                        break;
                    case "Start":
                        currentTask.Start = DateTime.Parse(properties[key]);
                        break;
                    case "Finish":
                        currentTask.Finish = DateTime.Parse(properties[key]);
                        break;
                    case "Task Mode":
                        if (properties[key].Trim().Equals("Yes"))
                        {
                            currentTask.TaskMode = Task_Mode.Yes;
                        }
                        else if (properties[key].Trim().Equals("No"))
                        {
                            currentTask.TaskMode = Task_Mode.No;
                        }
                        break;
                    case "Resource Names":
                        currentTask.ResourceNames.Add(properties[key]);
                        break;
                    case "Predecessors":
                        var task = getTask(int.Parse(properties[key]));
                        if (task == null )
                        {
                            currentTask.Predecessors.Add(new Task(int.Parse(properties[key])));
                        }
                        else
                        {
                            currentTask.Predecessors.Add(task);
                        }
                            break;
                    default:
                        break;
                }
            }
        }

        public override string ToString()
        {
            string msg = "";
            foreach (var t in tasks)
            {
                msg += t.ToString();
            }
            return msg;
        }
    }
}
