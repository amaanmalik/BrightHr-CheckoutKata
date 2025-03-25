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

    }
}
