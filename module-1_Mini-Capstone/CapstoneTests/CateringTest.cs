using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    public class CateringTest
    {
        [TestMethod]
        [DataRow(1,5,5)]
        [DataRow(100,500,50000)]
        [DataRow(1,0,0)]
        public void TotalPriceShouldSetRightProperty(int value1, int value2, double expected)
        {
            // Arrange 
            OrderedItems thing = new OrderedItems();

            // Act
            thing.OrderedQuantity = value1;
            thing.Price = value2;

            // Assert
            Assert.AreEqual(thing.totalPrice, (decimal)expected);
        }


    }
}
