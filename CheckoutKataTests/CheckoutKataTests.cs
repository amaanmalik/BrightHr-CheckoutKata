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
        private readonly List<IPricingRule> pricingRules = new List<IPricingRule>
        {
            new MultiBuyDiscountRule("A", 50, 3, 130),
            new MultiBuyDiscountRule("B", 30, 2, 45),
            new MultiBuyDiscountRule("C", 20),
            new MultiBuyDiscountRule("D", 15)
        };

        [Fact]
        public void Scan_SingleItemA_Returns50()
        {
            // Arrange
            ICheckout checkout = new Checkout(pricingRules);

            // Act
            checkout.Scan("A");

            // Assert
            Assert.Equal(50, checkout.GetTotalPrice());
        }

        [Fact]
        public void Scan_MultipleSameItems_ReturnsCorrectTotal()
        {
            // Arrange
            ICheckout checkout = new Checkout(pricingRules);

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
            ICheckout checkout = new Checkout(pricingRules);

            // Act
            checkout.Scan("A");
            checkout.Scan("B");

            // Assert
            Assert.Equal(80, checkout.GetTotalPrice()); // 50 (A) + 30 (B) = 80
        }

        [Theory]
        // Random combinations
        [InlineData("AABB", 145)]     // A(50) + A(50) + BB(45 -Discounted total) = 145
        [InlineData("ABC", 100)]      // A(50) + B(30) + C(20) = 100
        [InlineData("BCA", 100)]      // A(50) + B(30) + C(20) = 100
        [InlineData("CAB", 100)]      // A(50) + B(30) + C(20) = 100
        [InlineData("AAAABBC", 245)] // AAA(130 - Discounted total) + A(50) + BB(45 - Discounted total) + C(20) = 245 
                                     // Single items
        [InlineData("A", 50)]
        [InlineData("B", 30)]
        [InlineData("C", 20)]
        [InlineData("D", 15)]
        // Multiple same items (with discounts)
        [InlineData("AA", 100)]
        [InlineData("AAA", 130)]
        [InlineData("AAAA", 180)]
        [InlineData("AAAAA", 230)]
        [InlineData("AAAAAA", 260)]
        [InlineData("BB", 45)]
        [InlineData("BBB", 75)]
        [InlineData("BBBB", 90)]
        // Mixed items
        [InlineData("AB", 80)]
        [InlineData("ABCD", 115)]
        [InlineData("AAABB", 175)] // AAA(130) + BB(45)
        [InlineData("AAABBB", 205)] // AAA(130) + BBB(75)
        [InlineData("DABABA", 190)] // D(15) + AAA(130) + BB(45)
                                    // Different orderings     
        [InlineData("BAC", 100)]
        // Edge cases
        [InlineData("", 0)]
        [InlineData("X", 0)]  // Unknown item
        [InlineData("AXB", 80)] // A(50) + X(0) + B(30)
        [InlineData("AXXA", 100)] // AA(100) + XX(0)
        public void Scan_MultipleCombinations_ReturnsCorrectTotal(string items, int expectedTotal)
        {
            // Arrange
            ICheckout checkout = new Checkout(pricingRules);

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
            ICheckout checkout = new Checkout(pricingRules);

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
            ICheckout checkout = new Checkout(pricingRules);

            // Act
            for (int i = 0; i < quantity; i++)
            {
                checkout.Scan("A");
            }

            // Assert
            Assert.Equal(expectedTotal, checkout.GetTotalPrice());
        }

        [Fact]
        public void Scan_TwoBs_AppliesMultiBuyDiscount_Returns45()
        {
            // Arrange
            ICheckout checkout = new Checkout(pricingRules);

            // Act
            checkout.Scan("B");
            checkout.Scan("B");

            // Assert
            Assert.Equal(45, checkout.GetTotalPrice());  // 2B discount (45) instead of 2×30=60
        }

    }
}
