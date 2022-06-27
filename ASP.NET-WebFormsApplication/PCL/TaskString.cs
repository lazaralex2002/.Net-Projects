using System;
using System.Collections.Generic;
using System.Text;

namespace LazarAlexandruConstantin
{
    public class TaskString
    {
        public string TaskID { get; set; }
        public string Duration { get; set; }
        public string TaskName { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
        public string Predecessors { get; set; }
        public string ResourceNames { get; set; }
        public string TaskMode { get; set; }
    }
}