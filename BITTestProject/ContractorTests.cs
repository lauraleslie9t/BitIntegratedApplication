using BitDesktopApplication.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BITTestProject
{
    [TestClass]
    public class ContractorTests
    {
        [TestMethod]
        public void TestContractorFirstName()
        {
            Contractor contractor = new Contractor();
            contractor.FirstName = "John";
            Assert.AreEqual("John", contractor.FirstName);
        }
        [TestMethod]
        public void TestContractorLastName()
        {
            Contractor contractor = new Contractor();
            contractor.LastName = "Smith";
            Assert.AreEqual("Smith", contractor.LastName);
        }
        [TestMethod]
        public void TestContractorEmail()
        {
            Contractor contractor = new Contractor();
            contractor.Email = "johnsmith@gmail.com";
            Assert.AreEqual("johnsmith@gmail.com", contractor.Email);
        }
    }
}
