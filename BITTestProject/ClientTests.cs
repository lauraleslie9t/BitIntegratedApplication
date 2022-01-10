using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitDesktopApplication.Models;
using System;
using BitDesktopApplication.ViewModels;
using System.Collections.ObjectModel;
using Moq;

namespace BITTestProject
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void TestClientFirstName()
        {
            Client client = new Client();
            client.FirstName = "John";
            Assert.AreEqual("John", client.FirstName);
        }
        [TestMethod]
        public void TestClientLastName()
        {
            Client client = new Client();
            client.LastName = "Smith";
            Assert.AreEqual("Smith", client.LastName);
        }
        [TestMethod]
        public void TestClientEmail()
        {
            Client client = new Client();
            client.Email = "johnsmith@gmail.com";
            Assert.AreEqual("johnsmith@gmail.com", client.Email);
        }
        [TestMethod]
        public void TestClientCollection()
        {
            ClientManagementViewModel clientVM = new ClientManagementViewModel();
            int clientCount = clientVM.Clients.Count;

            Assert.AreEqual(clientCount, 5); //need to adjust after all test data is added. 
        }
        [TestMethod]
        public void TestClientCollectionMock()
        {
            ObservableCollection<Client> mockClientlist = new ObservableCollection<Client>();
            mockClientlist.Add(new Client
            {
                ClientId = 10,
                FirstName = "John",
                LastName = "Smith",
                Email = "johnSmith@gmail.com"
            });
            Mock<ClientManagementViewModel> mockClientVM = new Mock<ClientManagementViewModel>();
            mockClientVM.Setup(mc => mc.GetClients()).Returns(mockClientlist);
            int count = mockClientVM.Object.GetClients().Count;
            Assert.AreEqual(1, count);
        }
    }
}
