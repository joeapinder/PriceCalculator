using System.Collections.Generic;
using PriceCalculator.Models;

namespace PriceCalculator.Discounts
{
    /// <summary>
    /// Ideally I wanted just one method to do calculations.  But I need to see the state of the current basket in order to calculate discounts correctly when the discount is based on the presence of another item in
    /// the basket.  I could have used the GetGroupDiscount for both discounts, but I would be passing in an unused parameter.
    /// </summary>
    public interface IDiscount
    {
        Discount GetDiscount (BasketItem item);

        Discount GetGroupDiscount(BasketItem item, List<BasketItem> allBasketItems);

        DiscountType DiscountType { get; }
    }
}
