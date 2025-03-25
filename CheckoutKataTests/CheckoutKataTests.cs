using CheckoutKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKataTests
{
    public class CheckoutKataTests
    {
        [Fact]
        public void Scan_SingleItemA_Returns50()
        {
            // Arrange
            var checkout = new Checkout();

            // Act
            checkout.Scan("A");

            // Assert
            Assert.Equal(50, checkout.GetTotalPrice());
        }
        [Fact]
        public void Scan_MultipleSameItems_ReturnsCorrectTotal()
        {
            // Arrange
            ICheckout checkout = new Checkout();

            // Act
            checkout.Scan("A");
            checkout.Scan("A");

            // Assert
            Assert.Equal(100, checkout.GetTotalPrice());
        }

        [Fact]
        public void Scan_DifferentItems_ReturnsCorrectTotal()
        {
            // Arrange
            ICheckout checkout = new Checkout();

            // Act
            checkout.Scan("A");
            checkout.Scan("B");

            // Assert
            Assert.Equal(80, checkout.GetTotalPrice()); // 50 (A) + 30 (B) = 80
        }

        [Theory]
        [InlineData("AABB", 160)]     // A(50) + A(50) + B(30) + B(30) = 160
        [InlineData("ABC", 100)]      // A(50) + B(30) + C(20) = 100
        [InlineData("BCA", 100)]      // A(50) + B(30) + C(20) = 100
        [InlineData("CAB", 100)]      // A(50) + B(30) + C(20) = 100
        [InlineData("AAAABBC", 280)] // A(50*4) + B(30*2) + C(20) = 280 
        public void Scan_MultipleCombinations_ReturnsCorrectTotal(string items, int expectedTotal)
        {
            // Arrange
            ICheckout checkout = new Checkout();

            // Act
            foreach (char item in items)
            {
                checkout.Scan(item.ToString()); 
            }

            // Assert
            Assert.Equal(expectedTotal, checkout.GetTotalPrice());
        }

    }
}
