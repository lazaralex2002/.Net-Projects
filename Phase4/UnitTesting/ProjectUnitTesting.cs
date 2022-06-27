using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LazarAlexandruConstantin;

namespace UnitTesting
{
    [TestClass]
    public class ProjectUnitTesting
    {
        private readonly Project project;

        public ProjectUnitTesting()
        {
            project = new Project();
            project.Initialize("input.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void Initialize_WrongPath_ThrowException()
        {
            project.Initialize("asd.txt");
        }

        [TestMethod]
        [DataRow("SetTaskField Field:=\"Name\", Value:=\"UnitTesting\", TaskID:=1, ProjectName:=\"Project2\"")]
        public void AddTasks_InputLine_ReturnTrue(string line)//verifies addTaskProperties too
        {
            string[] lines = { line };
            project.AddTasks(lines);
            Assert.AreEqual(project.Tasks.GetTask(1).Name, "UnitTesting");
        }

        [TestMethod]
        public void Exists_Task_ReturnTrue()
        {
            Assert.IsTrue(project.Exists(new Task(1)));
        }

        [TestMethod]
        public void Exists_Task_ReturnFalse()
        {
            Assert.IsFalse(project.Exists(new Task(10)));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public void GetTask_Index_ReturnTrue(int index)
        {
            Assert.IsTrue(project.GetTask(index).Equals(new Task(index)));
        }
        [TestMethod]
        [DataRow(7)]
        [DataRow(-1)]
        public void GetTask_Index_ReturnNull(int index)
        {
            Assert.IsNull(project.GetTask(index));
        }

        [TestMethod]
        [DataRow("SetTaskField Field:=\"Name\", Value:=\"UnitTesting\", TaskID:=1, ProjectName:=\"Project2\"")]
        public void ToString_OneTaskProject_ReturnTrue(string line)
        {
            var project2 = new Project
            {
                Tasks = new HashSet<Task>()
            };
            string[] lines = { line};
            project2.AddTasks(lines);
            Assert.AreEqual(project2.ToString(), 
                "taskID: 1\n"+
                "name: UnitTesting\n"+
                "duration: 1\n"+
                $"start: {System.DateTime.Today}\n"+
                $"finish: {System.DateTime.Today}\n"+
                "resource names: \n"+
                "predecessors: \n"+
                "task mode: No\n\n");
        }
    }
}
