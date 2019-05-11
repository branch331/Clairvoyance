using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clairvoyance;
using Clairvoyance.ViewModel;
using Clairvoyance.Model;
using Clairvoyance.Data;
using Moq;

namespace ClairvoyanceTests
{
    [TestClass]
    public class StatisticsTests
    {
        private StatisticsViewModel statisticsVM;
        private TaskDatabaseLayer mockDbLayer;

        private DateTime fakeWeekStartDate = DateTime.Now.AddDays(-1);
        private DateTime fakeWeekEndDate = DateTime.Now.AddDays(1);

        [TestInitialize]
        public void setUpStatisticsVM()
        {
            var mockContext = new Mock<ITaskContext>();
            mockDbLayer = new TaskDatabaseLayer(mockContext.Object);

            var fakeTaskSet = new FakeDbSet<TaskItem>();

            var taskA = new TaskItem("Task A", "Cat A", "5", "6")
            {
                WeekId = 5,
                DayId = 0
            };

            var taskB = new TaskItem("Task B", "Cat B", "5", "6")
            {
                WeekId = 5,
                DayId = 3
            };

            fakeTaskSet.Add(taskA);
            fakeTaskSet.Add(taskB);

            mockContext.Setup(context => context.tasks).Returns(fakeTaskSet);

            var fakeWeekSet = new FakeDbSet<Day>();
            fakeWeekSet.Add(new Day("Mon") { Id = 0 });
            fakeWeekSet.Add(new Day("Tues") { Id = 1 });
            fakeWeekSet.Add(new Day("Wed") { Id = 2 });
            fakeWeekSet.Add(new Day("Thurs") { Id = 3 });
            fakeWeekSet.Add(new Day("Fri") { Id = 4 });
            fakeWeekSet.Add(new Day("Sat") { Id = 5 });
            fakeWeekSet.Add(new Day("Sun") { Id = 6 });

            mockContext.Setup(context => context.days).Returns(fakeWeekSet);

            var fakeWeekRanges = new FakeDbSet<Week>();
            fakeWeekRanges.Add(new Week(fakeWeekStartDate, fakeWeekEndDate)
            {
                Id = 5
            });

            mockContext.Setup(context => context.weeks).Returns(fakeWeekRanges);

            statisticsVM = new StatisticsViewModel(mockContext.Object);
        }

        [TestMethod]
        public void TestMockInitializeCategories()
        {
            statisticsVM.initializeCategoryTotals();

            Assert.IsTrue(statisticsVM.monCategoryTotals != null);
            Assert.IsTrue(statisticsVM.thursCategoryTotals != null);
        }

        [TestMethod]
        public void TestMockPopulateWeekRanges()
        {
            statisticsVM.populateAvailableWeekRangesFromDb();

            Assert.IsTrue(statisticsVM.availableWeekRanges[0].MondayDate == fakeWeekStartDate && statisticsVM.availableWeekRanges[0].SundayDate == fakeWeekEndDate);
        }

        [TestMethod]
        public void TestMockClearCategoryTotals()
        {
            statisticsVM.clearCategoryTotals();

            Assert.IsTrue(statisticsVM.monCategoryTotals.Count == 0 && statisticsVM.thursCategoryTotals.Count == 0);
        }

        [TestMethod]
        public void TestMockLoadWeekData()
        {
            statisticsVM.loadWeekDataFromDb();

            Assert.IsTrue(statisticsVM.monCategoryTotals[0].TotalHours == 1 && statisticsVM.thursCategoryTotals[0].TotalHours == 1);
        }

        [TestMethod]
        public void TestMockIsWithinCategoryTotalsTrue()
        {
            statisticsVM.loadWeekDataFromDb();

            Assert.IsTrue(statisticsVM.isWithinCategoryTotals("Cat A", statisticsVM.monCategoryTotals));
            Assert.IsTrue(statisticsVM.isWithinCategoryTotals("Cat B", statisticsVM.thursCategoryTotals));
        }

        [TestMethod]
        public void TestMockIsWithinCategoryTotalsFalse()
        {
            statisticsVM.loadWeekDataFromDb();

            Assert.IsFalse(statisticsVM.isWithinCategoryTotals("Unreal Category", statisticsVM.monCategoryTotals));
            Assert.IsFalse(statisticsVM.isWithinCategoryTotals("Unreal Category", statisticsVM.thursCategoryTotals));
        }
    }
}
