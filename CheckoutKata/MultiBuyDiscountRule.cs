using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    /// <summary>
    /// Pricing rule that applies multi-buy discounts (e.g., "3 for 100" type offers)
    /// </summary>
    public class MultiBuyDiscountRule : IPricingRule
    {
        private readonly string sku;               // The product identifier this rule applies to
        private readonly int unitPrice;           // Individual item price without discount
        private readonly int discountQuantity;     // Quantity needed to qualify for discount
        private readonly int discountPrice;       // Special price for the discounted quantity

        /// <summary>
        /// Initializes a new multi-buy discount rule
        /// </summary>
        /// <param name="sku">Product identifier this rule applies to</param>
        /// <param name="unitPrice">Standard price per unit</param>
        /// <param name="discountQuantity">
        ///     Number of items required to qualify for discount (default 1 = no discount)
        /// </param>
        /// <param name="discountPrice">
        ///     Special price for the discount quantity. If 0, calculates as unitPrice*discountQuantity
        /// </param>
        public MultiBuyDiscountRule(string sku, int unitPrice, int discountQuantity = 1, int discountPrice = 0)
        {
            this.sku = sku;
            this.unitPrice = unitPrice;
            this.discountQuantity = discountQuantity;
            this.discountPrice = discountPrice == 0 ? unitPrice * discountQuantity : discountPrice;
        }

        /// <summary>
        /// Calculates the total price for all items of this SKU, applying multi-buy discounts where applicable
        /// </summary>
        /// <param name="items">All scanned items in the checkout</param>
        /// <returns>Total price for items of this SKU after discount rules applied</returns>
        public int Apply(List<string> items)
        {
            // Count how many items match our SKU
            int count = items.Count(item => item == sku);

            // If no discount configured (discountQuantity = 1), use standard pricing
            if (discountQuantity == 1)
            {
                return count * unitPrice;
            }

            // Calculate how many complete discount groups we have
            int discountGroups = count / discountQuantity;

            // Calculate how many items remain after applying discount groups
            int remainingItems = count % discountQuantity;

            // Total = (discounted groups) + (remaining individual items)
            return (discountGroups * discountPrice) + (remainingItems * unitPrice);
        }
    }
}