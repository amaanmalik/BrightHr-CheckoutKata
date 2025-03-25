using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    /// <summary>
    /// Implementation of a checkout system that calculates total price 
    /// based on scanned items and applied pricing rules.
    /// </summary>
    public class Checkout : ICheckout
    {
        // List to keep track of all scanned items
        private readonly List<string> scannedItems = new List<string>();

        // List of pricing rules to be applied during total calculation
        private readonly List<IPricingRule> pricingRules;

        /// <summary>
        /// Initializes a new instance of the Checkout class with specified pricing rules.
        /// </summary>
        /// <param name="pricingRules">The pricing rules to apply during checkout</param>
        public Checkout(List<IPricingRule> pricingRules)
        {
            this.pricingRules = pricingRules;
        }

        /// <summary>
        /// Scans an item and adds it to the list of items for checkout.
        /// </summary>
        /// <param name="sku">The stock keeping unit (SKU) of the item</param>
        public void Scan(string sku)
        {
            scannedItems.Add(sku);
        }

        /// <summary>
        /// Calculates the total price of all scanned items by applying all pricing rules.
        /// </summary>
        /// <returns>The total price after applying all pricing rules</returns>
        public int GetTotalPrice()
        {
            int total = 0;

            // Apply each pricing rule to the scanned items and accumulate the total
            foreach (var rule in pricingRules)
            {
                total += rule.Apply(scannedItems);
            }

            return total;
        }
    }
}