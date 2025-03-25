using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    /// <summary>
    /// Interface defining the contract for pricing rules that can be applied during checkout.
    /// </summary>
    public interface IPricingRule
    {
        /// <summary>
        /// Applies the pricing rule to a collection of scanned items.
        /// </summary>
        /// <param name="items">List of scanned item SKUs to which the rule should be applied</param>
        /// <returns>The calculated price for these items after applying the rule</returns>
        int Apply(List<string> items);
    }
}