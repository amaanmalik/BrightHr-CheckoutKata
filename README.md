
# BrightHr-CheckoutKata

**Implementation Details**
The solution consists of three main components:

 **- ICheckout Interface:**

> public interface ICheckout {
>     void Scan(string item);
>     int GetTotalPrice(); }

 **- Checkout Class:**

Maintains a list of scanned items
       Applies all pricing rules when calculating total
       Handles items in any order

**-MultiBuyDiscountRule Class:**

Implements special offer pricing logic

Flexible enough to handle both regular and discounted items

Calculates optimal pricing for mixed quantities

**Example Usage**

    // Configure pricing rules
    var pricingRules = new List<IPricingRule>
    {
        new MultiBuyDiscountRule("A", 50, 3, 130),
        new MultiBuyDiscountRule("B", 30, 2, 45),
        new MultiBuyDiscountRule("C", 20),
        new MultiBuyDiscountRule("D", 15)
    };
    
    // Create checkout instance
    var checkout = new Checkout(pricingRules);
    
    // Scan items in any order
    checkout.Scan("A");
    checkout.Scan("B");
    checkout.Scan("A");
    checkout.Scan("C");
    
    // Get total with discounts applied
    int total = checkout.GetTotalPrice(); // Returns 145

**Test Approach**
The solution was developed using Test-Driven Development (TDD) with comprehensive unit tests covering:

*Individual item pricing
Special offer pricing
Mixed scenarios with both discounted and non-discounted items
Edge cases (empty cart, unknown SKUs, etc.*)

**Design Considerations**
*Flexibility:*

Pricing rules can be easily modified or extended

New special offer types can be added by implementing IPricingRule

*Performance:*

Optimized for typical supermarket basket sizes

Efficient scanning and total calculation

*Extensibility:*

Clean interface makes it easy to add new features

Separation of concerns between checkout and pricing rules
