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
        [InlineData("AAAABBC", 260)] // AAA(130 - Discounted total) + A(50) B(30*2) + C(20) = 260 
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

        [Fact]
        public void Scan_ThreeAs_AppliesMultiBuyDiscount_Returns130()
        {
            // Arrange
            ICheckout checkout = new Checkout();

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            // Assert
            Assert.Equal(130, checkout.GetTotalPrice());  // 3A discount (130) instead of 3×50=150
        }

        [Theory]
        [InlineData(3, 130)]    // 3A = 130 (discount only)
        [InlineData(4, 180)]    // 3A + 1A = 130 + 50
        [InlineData(5, 230)]    // 3A + 2A = 130 + 100
        [InlineData(6, 260)]    // 3A + 3A = 130 + 130
        [InlineData(7, 310)]    // 3A + 3A + 1A = 130 + 130 + 50
        public void Scan_MultipleAs_AppliesCorrectDiscounts(int quantity, int expectedTotal)
        {
            // Arrange
            ICheckout checkout = new Checkout();

            // Act
            for (int i = 0; i < quantity; i++)
            {
                checkout.Scan("A");
            }

            // Assert
            Assert.Equal(expectedTotal, checkout.GetTotalPrice());
        }

    }
}
