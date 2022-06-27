using System;
using System.Collections.Generic;

namespace LazarAlexandruConstantin
{
    public class Task
    {
        public int TaskID { get; set; }
        public string Name { get; set; }
        public double Duration { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public HashSet<Task> Predecessors { get; set; }
        public HashSet<string> ResourceNames { get; set; }
        public Task_Mode TaskMode;

        private Task()
        {

        }

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
                msg += pred + " ";
            }
            msg += '\n';
            msg += "task mode: " + TaskMode + '\n';
            msg += '\n';
            return msg;
        }

        public override int GetHashCode()//functia este esentiala pentru functionarea corecta a HashSet-ului.
        {
            return TaskID;
        }
    }

    public enum Task_Mode
    {
        Yes,
        No
    };
}
