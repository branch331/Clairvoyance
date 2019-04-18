using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clairvoyance;

namespace ClairvoyanceTests
{
    [TestClass]
    public class TaskItemTests
    {
        TaskItemModel taskItemObject;

        [TestInitialize]
        public void setUpTaskItemObject()
        {
            taskItemObject = new TaskItemModel("TestName", "TestCategory", "4:30", "5:30");
        }

        [TestMethod]
        public void TestTaskName()
        {
            Assert.IsTrue(taskItemObject.TaskName == "TestName");
        }

        [TestMethod]
        public void TestTaskCategory()
        {
            Assert.IsTrue(taskItemObject.TaskCategory == "TestCategory");
        }

        [TestMethod]
        public void TestTaskTimeIntervalOneHour()
        {
            Assert.IsTrue(taskItemObject.TaskTimeInterval.Hours == 1);
        }

        [TestMethod]
        public void TestTaskTimeIntervalTwoHours()
        {
            taskItemObject.TaskStartDateTime = Convert.ToDateTime("4:00");
            taskItemObject.TaskEndDateTime = Convert.ToDateTime("6:00");
            taskItemObject.TaskTimeInterval = taskItemObject.TaskEndDateTime - taskItemObject.TaskStartDateTime;
            Assert.IsTrue(taskItemObject.TaskTimeInterval.Hours == 2);
        }

        [TestMethod]
        public void TestTaskIntegerTimeInputs()
        {
            TaskItemModel newTaskItemObject = new TaskItemModel("", "", "5", "9");
            Assert.IsTrue(newTaskItemObject.TaskTimeInterval.Hours == 4);
        }
    }
}
