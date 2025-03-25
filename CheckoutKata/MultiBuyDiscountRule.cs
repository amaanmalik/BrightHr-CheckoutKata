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

        public MultiBuyDiscountRule(string sku, int unitPrice, int discountQuantity, int discountPrice)
        {
            this.sku = sku;
            this.unitPrice = unitPrice;
            this.discountQuantity = discountQuantity;
            this.discountPrice = discountPrice;
        }

        public int Apply(List<string> items)
        {
            int count = items.Count(item => item == sku);
            int discountTotal = (count / discountQuantity) * discountPrice;
            int regularTotal = (count % discountQuantity) * unitPrice;

            return discountTotal + regularTotal;
        }
    }
}
