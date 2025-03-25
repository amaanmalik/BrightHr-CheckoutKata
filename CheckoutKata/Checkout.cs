using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private readonly List<string> scannedItems = new List<string>();
        private readonly List<IPricingRule> pricingRules;

        public Checkout(List<IPricingRule> pricingRules)
        {
            this.pricingRules = pricingRules;
        }

        public void Scan(string sku)
        {
            scannedItems.Add(sku);
        }

        public int GetTotalPrice()
        {
            int total = 0;

            foreach (var rule in pricingRules)
            {
                total += rule.Apply(scannedItems);
            }

            return total;
        }
    }
}
