using LazarAlexandruConstantin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace UnitTesting
{
    [TestClass]
    public class TasksUnitTesting
    {
        private readonly Project project;
        public TasksUnitTesting()
        {
            project = new Project();
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;
            project.Initialize(startupPath + "\\input.txt");
        }

        [TestMethod]
        [DataRow("-1")]
        [DataRow("0")]
        [DataRow("123")]
        public void IsInteger_Integers_ReturnTrue(string value)
        {
            bool result = value.IsInteger();
            Assert.IsTrue(result, $"{value} is an integer");
        }

        [TestMethod]
        [DataRow("-001")]
        [DataRow("001")]
        public void IsInteger_Numbers_ReturnFaslse(string value)
        {
            bool result = value.IsInteger();
            Assert.IsFalse(result, $"{value} is not an integer");
        }

        [TestMethod]
        [DataRow("")]
        public void IsInteger_EmptyString_ReturnFaslse(string value)
        {
            bool result = value.IsInteger();
            Assert.IsFalse(result, "Empty String should not be an integer");
        }


        [TestMethod]
        public void ContentToString_HashSetWithStrings_ReturnTrue()
        {
            var set = new HashSet<string>() { "resource 1", "resource 2" };
            Assert.AreEqual(set.ContentToString(), "resource 1 resource 2 ");
        }

        [TestMethod]
        public void ContentToString_HashSetWithTasks_ReturnTrue()
        {
            var set = new HashSet<Task>() { new Task(1), new Task(2) };
            Assert.AreEqual(set.ContentToString(), "1 2 ");
        }

        [TestMethod]
        public void GetTask_HashSetWithTasks_ReturnTrue()
        {
            var set = new HashSet<Task>() { new Task(1), new Task(2) };
            Assert.AreEqual(set.GetTask(1), new Task(1));
        }

        [TestMethod]
        public void GetTask_HashSetWithTasks_ReturnFalse()
        {
            var set = new HashSet<Task>() { new Task(1), new Task(2) };
            Assert.AreNotEqual(set.GetTask(1), new Task(2));
        }

        [TestMethod]
        public void GetTask_HashSetWithTasks_ReturnNull()
        {
            var set = new HashSet<Task>() { new Task(1), new Task(2) };
            Assert.IsNull(set.GetTask(3));
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Deserialize_WrongPath_ThrowException()
        {
            project.Deserialize("program.xml");
        }
    }
}
