using System;
using System.Collections.Generic;
using System.Text;

namespace Interface
{
    public class Task
    {
        public int TaskID { get; }
        public string Name { get; set; }
        public double Duration { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public HashSet<Task> Predecessors { get; set; }
        public HashSet<string> ResourceNames { get; set; }
        public Task_Mode TaskMode { get; set; }

        public Task(int taskId)
        {
            this.TaskID = taskId;
            this.Duration = 1;
            this.Start = System.DateTime.Today;
            this.Finish = System.DateTime.Today;
            TaskMode = Task_Mode.No;
            Predecessors = new HashSet<Task>();
            ResourceNames = new HashSet<string>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            Task t = (Task)obj;
            return this.TaskID == t.TaskID;
        }

        public override string ToString()
        {
            string msg = "";
            msg += "taskID: " + TaskID + '\n';
            msg += "name: " + Name + '\n';
            msg += "duration: " + Duration + '\n';
            msg += "start: " + Start + '\n';
            msg += "finish: " + Finish + '\n';
            msg += "resource names: ";
            foreach (var resouce in ResourceNames)
            {
                msg += resouce + " ";
            }
            msg += '\n';
            msg += "predecessors: ";
            foreach (var pred in Predecessors)
            {
                msg += pred.TaskID + " ";
            }
            msg += '\n';
            msg += "task mode: " + TaskMode + '\n';
            msg += '\n';
            return msg;
        }

        public override int GetHashCode()
        {
            return TaskID;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public enum Task_Mode
    {
        Yes,
        No
    };
}
