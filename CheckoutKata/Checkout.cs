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
            switch (sku)
            {
                case "A":
                    total += 50;
                    break;
                case "B":
                    total += 30;
                    break;
                case "C":
                    total += 20;
                    break;
                case "D":
                    total += 15;
                    break;
            }
        }

        public int GetTotalPrice() => total;

    }
}
