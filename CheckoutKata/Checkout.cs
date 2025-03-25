using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private List<string> scannedItems = new List<string>();

        public void Scan(string sku)
        {
            scannedItems.Add(sku);
        }

        private int ApplyMultiBuyDiscount(string sku, int unitPrice, int discountQuantity, int discountPrice)
        {
            int count = scannedItems.Count(item => item == sku);
            int discountTotal = (count / discountQuantity) * discountPrice;
            int regularTotal = (count % discountQuantity) * unitPrice;

            return discountTotal + regularTotal;
        }

        public int GetTotalPrice()
        {
            int total = 0;

            total += ApplyMultiBuyDiscount("A", 50, 3, 130); // Discount: 3 for 130
            total += ApplyMultiBuyDiscount("B", 30, 2, 45); // Discount: 2 for 45
            total += scannedItems.Count(sku => sku == "C") * 20;
            total += scannedItems.Count(sku => sku == "D") * 15;

            return total;
        }
    }
}
