using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    public class CateringTest
    {
        [TestMethod]
        public void CateringInstanceShouldBeCreated()
        {
            // Arrange 
            Catering catering = new Catering();

            // Act

            // Assert
            Assert.IsNotNull(catering);
        }
    }
}
