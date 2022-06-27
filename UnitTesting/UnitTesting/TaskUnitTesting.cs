using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LazarAlexandruConstantin;

namespace UnitTesting
{
    [TestClass]
    public class TaskUnitTesting
    {
        private readonly Task task;

        public TaskUnitTesting()
        {
            task = new Task(1);
        }

        [TestMethod]
        public void Equals_Task_ReturnTrue()
        {
            Assert.IsTrue(task.Equals(new Task(1)));
        }

        [TestMethod]
        public void Equals_Task_ReturnFalse()
        {
            Assert.IsFalse(task.Equals(new Task(2)));
        }

        [TestMethod]
        public void Equals_Object_ReturnFalse()
        {
            Assert.IsFalse(task.Equals(new Object()));
        }

        [TestMethod]
        public void Equals_Null_ReturnFalse()
        {
            Assert.IsFalse(task.Equals(null));
        }

        [TestMethod]
        public void ToString__ReturnTrue()
        {
            Assert.AreEqual(task.ToString(),
                $"taskID: {task.TaskID}\n" +
                $"name: {task.Name}\n" +
                $"duration: {task.Duration}\n" +
                $"start: {task.Start}\n" +
                $"finish: {task.Finish}\n" +
                $"resource names: {task.ResourceNames.ContentToString()}\n" +
                $"predecessors: {task.Predecessors.ContentToString()}\n" +
                $"task mode: {task.TaskMode}\n\n"
                );
        }

        [TestMethod]
        public void GetHashCode__ReturnTrue()
        {
            Assert.AreEqual(task.GetHashCode(), task.TaskID);
        }
    }
}
