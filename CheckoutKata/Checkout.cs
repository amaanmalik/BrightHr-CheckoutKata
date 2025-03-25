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

        public int GetTotalPrice()
        {
            int total = 0;

            // Count occurrences of each SKU
            int countA = scannedItems.Count(sku => sku == "A");
            int countB = scannedItems.Count(sku => sku == "B");
            int countC = scannedItems.Count(sku => sku == "C");
            int countD = scannedItems.Count(sku => sku == "D");

            // Apply multi-buy discount for A (3 for 130)
            total += (countA / 3) * 130;               // Apply discount for every set of 3 A's
            total += (countA % 3) * 50;                // Add remaining A's at regular price

            // Apply multi-buy discount for B (3 for 130)
            total += (countB / 2) * 45;               // Apply discount for every set of 2 B's
            total += (countB % 2) * 30;                // Add remaining B's at regular price
            
            // Regular pricing for C and D
            total += countC * 20;
            total += countD * 15;


            return total;

        }
    }
}
