using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private int total = 0;

        public void Scan(string sku)
        {
            if (sku == "A")
                total += 50;
            else if (sku == "B")
                total += 30;
        }

        public int GetTotalPrice() => total;

    }
}
