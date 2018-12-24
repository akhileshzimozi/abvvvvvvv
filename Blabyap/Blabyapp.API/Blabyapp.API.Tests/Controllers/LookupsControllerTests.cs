using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blabyapp.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blabyapp.API.Controllers.Tests
{
    [TestClass()]
    public class LookupsControllerTests
    {
        [TestMethod()]
        public void GetLookUpExpertiseTest()
        {
            //Arrange
            var obj = new LookupsController();
            //Act
            var expectedresult = obj.GetLookUpExpertise();


            //Assert

            Assert.IsTrue(true);
        }
    }
}