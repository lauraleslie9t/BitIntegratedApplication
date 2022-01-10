using BitDesktopApplication.Models;
using BitDesktopApplication.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.ObjectModel;

namespace BITTestProject
{
    [TestClass]
    public class CoordinatorTests
    {
        [TestMethod]
        public void TestCoordinatorFirstName()
        {
            Coordinator client = new Coordinator();
            client.FirstName = "John";
            Assert.AreEqual("John", client.FirstName);
        }
        [TestMethod]
        public void TestCoordinatorLastName()
        {
            Coordinator coordinator = new Coordinator();
            coordinator.LastName = "Smith";
            Assert.AreEqual("Smith", coordinator.LastName);
        }
        [TestMethod]
        public void TestCoordinatorEmail()
        {
            Coordinator coordinator = new Coordinator();
            coordinator.Email = "johnsmith@gmail.com";
            Assert.AreEqual("johnsmith@gmail.com", coordinator.Email);
        }
        [TestMethod]
        public void TestCoordinatorCollection()
        {
            CoordinatorManagementViewModel coordinatorVM = new CoordinatorManagementViewModel();
            int coordCount = coordinatorVM.Coordinators.Count;

            Assert.AreEqual(coordCount, 9); //need to adjust after all test data is added. 
        }
        [TestMethod]
        public void TestContractorCollectionMock()
        {
            ObservableCollection<Coordinator> mockCoordinatorlist = new ObservableCollection<Coordinator>();
            mockCoordinatorlist.Add(new Coordinator
            {
                StaffId = 10,
                FirstName = "John",
                LastName = "Smith",
                Email = "johnSmith@gmail.com"
            });
            Mock<CoordinatorManagementViewModel> mockCoordinatorVM = new Mock<CoordinatorManagementViewModel>();
            mockCoordinatorVM.Setup(mc => mc.GetCoordinators()).Returns(mockCoordinatorlist);
            int count = mockCoordinatorVM.Object.GetCoordinators().Count;
            Assert.AreEqual(1, count);
        }
    }
}
