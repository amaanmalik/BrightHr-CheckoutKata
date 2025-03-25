using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    public class MultiBuyDiscountRule : IPricingRule
    {
        private readonly string sku;
        private readonly int unitPrice;
        private readonly int discountQuantity;
        private readonly int discountPrice;

        public MultiBuyDiscountRule(string sku, int unitPrice, int discountQuantity = 1, int discountPrice = 0)
        {
            this.sku = sku;
            this.unitPrice = unitPrice;
            this.discountQuantity = discountQuantity;
            this.discountPrice = discountPrice == 0 ? unitPrice * discountQuantity : discountPrice;
        }

        public int Apply(List<string> items)
        {
            int count = items.Count(item => item == sku);

            // If no discount (discountQuantity = 1), just return unit price * count
            if (discountQuantity == 1)
            {
                return count * unitPrice;
            }

            int discountGroups = count / discountQuantity;
            int remainingItems = count % discountQuantity;

            return (discountGroups * discountPrice) + (remainingItems * unitPrice);
        }
    }
}
