using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    /// <summary>
    /// Interface defining the contract for a checkout system.
    /// </summary>
    public interface ICheckout
    {
        /// <summary>
        /// Scans an item and adds it to the checkout.
        /// </summary>
        /// <param name="item">The item to be scanned (typically identified by SKU)</param>
        void Scan(string item);

        /// <summary>
        /// Calculates the total price of all scanned items.
        /// </summary>
        /// <returns>The total price after applying all applicable pricing rules</returns>
        int GetTotalPrice();
    }
}