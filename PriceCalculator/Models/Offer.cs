using PriceCalculator.Discounts;

namespace PriceCalculator.Models
{
    public class Offer
    {
        public Offer(string goodsItemName, IDiscount discount)
        {
            GoodsItemName = goodsItemName;
            Discount = discount;
        }
        public string GoodsItemName { get; }

        public IDiscount Discount { get; }
    }
}
