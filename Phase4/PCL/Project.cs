using System;
using System.Collections.Generic;
using System.IO;

namespace LazarAlexandruConstantin
{
    public class Project
    {
        public HashSet<Task> Tasks { get; set; }

        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        public void Initialize(string fileName)
        {
            Tasks = new HashSet<Task>();
            string fullName = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + '\\' + fileName;
            string[] lines;
            try
            {
                lines = System.IO.File.ReadAllLines(fullName);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"{fullName} was not found");
            }
            catch (Exception e)
            {
                throw e;
            }
            if (lines != null)
            {
                AddTasks(lines);
            }
        }

        public void AddTasks(string[] lines)
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
                        Tasks.Add(currentTask);
                    }
                    else
                    {
                        foreach (var t in Tasks)
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
        public void AddTaskProperties(Task currentTask, Dictionary<string, string> properties)
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
                        currentTask.ResourceNames = new HashSet<string>
                        {
                            properties[key]
                        };
                        break;
                    case "Predecessors":
                        var task = GetTask(int.Parse(properties[key]));
                        if (task == null)
                        {
                            currentTask.Predecessors = new HashSet<Task>()
                            {
                                new Task(int.Parse(properties[key]))
                            };
                        }
                        else
                        {
                            currentTask.Predecessors = new HashSet<Task>
                            {
                                task
                            };
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public bool Exists(Task currentTask)
        {
            if (Tasks.Contains(currentTask))
            {
                return true;
            }
            return false;
        }

        public Task GetTask(int index)
        {
            foreach (var t in Tasks)
            {
                if (t.TaskID == index) return t;
            }
            return null;
        }

        public override string ToString()
        {
            string msg = "";
            foreach (var t in Tasks)
            {
                msg += t.ToString();
            }
            return msg;
        }
    }
}
