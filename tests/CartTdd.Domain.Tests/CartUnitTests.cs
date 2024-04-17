using CardTdd.Domain.CartAggregate;

namespace CartTdd.Domain.Tests
{
    public class CartUnitTests
    {

        private readonly Cart _cart;

        public CartUnitTests()
        {
            _cart = new Cart();
        }

        [Fact]
        public void Should_BeEmptyCart_When_CreatedCard()
        {
            Assert.Equal(0, _cart.TotalPrice);
            Assert.Empty(_cart.Products);
        }

        [Fact]
        public void Should_Succeed_When_AddProduct()
        {
            _cart.AddProduct(new CardProduct("sku1", 10, 100));
            _cart.AddProduct(new CardProduct("sku2", 20, 200));

            Assert.Equal(5000, _cart.TotalPrice);
            Assert.Equal(2, _cart.Products.Count);

            var product1 = _cart.Products[0];
            Assert.Equal("sku1", product1.Sku);
            Assert.Equal(10, product1.Quantity);
            Assert.Equal(100, product1.Price);

            var product2 = _cart.Products[1];
            Assert.Equal("sku2", product2.Sku);
            Assert.Equal(20, product2.Quantity);
            Assert.Equal(200, product2.Price);

        }


        [Fact]
        public void Should_Succeed_When_RemoveProduct()
        {
            _cart.AddProduct(new CardProduct("sku1", 10, 100));
            _cart.AddProduct(new CardProduct("sku2", 20, 200));

            _cart.RemoveProduct("sku2");

            Assert.Equal(1000, _cart.TotalPrice);
            Assert.Single(_cart.Products);

            var product1 = _cart.Products[0];
            Assert.Equal("sku1", product1.Sku);
            Assert.Equal(10, product1.Quantity);
            Assert.Equal(100, product1.Price);
        }

        [Fact]
        public void Should_ThrowException_When_RemoveProduct_If_ProductIsNotFound()
        {
            _cart.AddProduct(new CardProduct("sku1", 10, 100));
            _cart.AddProduct(new CardProduct("sku2", 20, 200));

            var exception = Assert.Throws<CartProductIsNotFoundException>(() => _cart.RemoveProduct("sku3"));
            Assert.Equal("The product is not found!", exception.Message);
        }

        [Fact]
        public void Should_Succeed_When_IncreaseProductQuantity()
        {
            _cart.AddProduct(new CardProduct("sku1", 10, 100));
            _cart.AddProduct(new CardProduct("sku2", 20, 200));
            _cart.IncreaseProductQuantity("sku2");

            var product1 = _cart.Products[0];
            Assert.Equal("sku1", product1.Sku);
            Assert.Equal(10, product1.Quantity);
            Assert.Equal(100, product1.Price);

            var product2 = _cart.Products[1];
            Assert.Equal("sku2", product2.Sku);
            Assert.Equal(21, product2.Quantity);
            Assert.Equal(200, product2.Price);

            Assert.Equal(5200, _cart.TotalPrice);
        }

        [Fact]
        public void Should_ThrowException_When_IncreaseProductQuantity_If_ProductIsNotFound()
        {
            _cart.AddProduct(new CardProduct("sku1", 10, 100));

            var exception = Assert.Throws<CartProductIsNotFoundException>(() => _cart.IncreaseProductQuantity("sku3"));
            Assert.Equal("The product is not found!", exception.Message);

        }


        [Fact]
        public void Should_Succeed_When_DecreaseProductQuantity()
        {
            _cart.AddProduct(new CardProduct("sku1", 10, 100));
            _cart.AddProduct(new CardProduct("sku2", 20, 200));
            _cart.DecreaseProductQuantity("sku2");

            var product1 = _cart.Products[0];
            Assert.Equal("sku1", product1.Sku);
            Assert.Equal(10, product1.Quantity);
            Assert.Equal(100, product1.Price);

            var product2 = _cart.Products[1];
            Assert.Equal("sku2", product2.Sku);
            Assert.Equal(19, product2.Quantity);
            Assert.Equal(200, product2.Price);

            Assert.Equal(4800, _cart.TotalPrice);
        }

        [Fact]
        public void Should_ThrowException_When_DecreaseProductQuantity_If_ProductIsNotFound()
        {
            _cart.AddProduct(new CardProduct("sku1", 10, 100));

            var exception = Assert.Throws<CartProductIsNotFoundException>(() => _cart.DecreaseProductQuantity("sku3"));
            Assert.Equal("The product is not found!", exception.Message);

        }

        [Fact]
        public void Should_RemoveProduct_When_DecreaseProductQuantity_If_ProductQuantityIsOne()
        {
            _cart.AddProduct(new CardProduct("sku1", 10, 100));
            _cart.AddProduct(new CardProduct("sku2", 1, 200));
            _cart.DecreaseProductQuantity("sku2");

            Assert.Single(_cart.Products);
            Assert.Equal(1000, _cart.TotalPrice);
            var product2 = _cart.Products[0];
            Assert.Equal("sku1", product2.Sku);
            Assert.Equal(10, product2.Quantity);
            Assert.Equal(100, product2.Price);
        }

        [Fact]
        public void Should_Be_CartProductsListEmptyAndTotalPriceZero_When_ClearProducts()
        {
            _cart.AddProduct(new CardProduct("sku1", 10, 100));
            _cart.AddProduct(new CardProduct("sku2", 1, 200));

            _cart.Clear();

            Assert.Empty(_cart.Products);
            Assert.Equal(0, _cart.TotalPrice);


        }


    }
}