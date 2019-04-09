using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clairvoyance;

namespace ClairvoyanceTests
{
    [TestClass]
    public class WeeklyAgendaTests
    {
        [TestMethod]
        public void WorkWeekTestCount()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel();
            testAgendaVM.isWorkWeek = true;
            Assert.IsTrue(testAgendaVM.generateDaysToDisplayList().Count == 5);
        }

        [TestMethod]
        public void FullWeekTestCount()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel();
            testAgendaVM.isWorkWeek = false;
            Assert.IsTrue(testAgendaVM.generateDaysToDisplayList().Count == 7);
        }

        [TestMethod]
        public void WorkWeekTestFirstObject()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel();
            testAgendaVM.isWorkWeek = true;

            Assert.IsTrue(testAgendaVM.generateDaysToDisplayList()[0].NameOfDay == "Mon");
        }

        [TestMethod]
        public void FullWeekTestFirstObject()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel();
            testAgendaVM.isWorkWeek = true;

            Assert.IsTrue(testAgendaVM.generateDaysToDisplayList()[0].NameOfDay == "Mon");
        }

        [TestMethod]
        public void WorkWeekTestLastObject()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel();
            testAgendaVM.isWorkWeek = true;

            List<DayPlannerModel> workWeekList = new List<DayPlannerModel>();
            workWeekList = testAgendaVM.generateDaysToDisplayList();

            Assert.IsTrue(workWeekList[workWeekList.Count - 1].NameOfDay == "Fri");
        }

        [TestMethod]
        public void FullWeekTestLastObject()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel();
            testAgendaVM.isWorkWeek = true;

            List<DayPlannerModel> fullWeekList = new List<DayPlannerModel>();
            fullWeekList = testAgendaVM.generateDaysToDisplayList();

            Assert.IsTrue(fullWeekList[fullWeekList.Count - 1].NameOfDay == "Fri");
        }
    }
}
