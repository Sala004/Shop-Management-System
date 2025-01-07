using Bie_Shop.General;
using Bie_Shop.ProductManagement;

namespace ProductTests
{
    public class ProductTests
    {

        [Fact]
        public void UseProduct_Reduce_AmountInStock()
        {
            //Arrange
            Product product = new Product(1, "Sugar", "Lorem ipum", new Price()
            { Currency = Currency.Euro, itemPrice = 10 }, UnitType.PerKg, 100);

            product.IncreaseStock(100);

            //Act
            product.UseProduct(20);

            //Assert
            Assert.Equal(80, product.AmountInStock);
        }

        [Fact]
        public void UseProduct_ItemsHigherThanStock_NoChangeToStock()
        {
            //Arrange
            Product product = new Product(1, "Sugar", "Lorem ipum", new Price()
            { Currency = Currency.Euro, itemPrice = 10 }, UnitType.PerKg, 100);

            product.IncreaseStock(10);

            //Act
            product.UseProduct(100);

            //Assert
            Assert.Equal(10, product.AmountInStock);
        }

        [Fact]
        public void UseProduct_Reduce_AmountInStock_StockBelowThreshold()
        {
            //Arrange
            Product product = new Product(1, "Sugar", "Lorem ipum", new Price()
            { Currency = Currency.Euro, itemPrice = 10 }, UnitType.PerKg, 100);

            int increaseValue = 100;
            product.IncreaseStock(increaseValue);

            //Act
            product.UseProduct(increaseValue - 1);

            //Assert
            Assert.True(product.IsBelowStockThreshold);
        }
    }
}