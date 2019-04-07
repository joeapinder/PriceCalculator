using System.Collections.Generic;
using PriceCalculator.Discounts;
using PriceCalculator.Models;

namespace PriceCalculator
{
    public class PriceCalculatorConfiguration
    {
        private List<Offer> _availableOffers;
        private List<GoodsItem> _availableGoods;
        public PriceCalculatorConfiguration()
        {
            _availableGoods = new List<GoodsItem>()
            {
                new GoodsItem("Beans", 0.65m),
                new GoodsItem("Bread", 0.80m),
                new GoodsItem("Milk", 1.30m),
                new GoodsItem("Apples", 1.00m),
            };

            _availableOffers = new List<Offer>()
            {
                new Offer("Apples", new PercentageDiscount(10)),
                new Offer("Bread", new PercentageDiscountForPurchasingOtherItems(50, "Beans", 2))
            };
        }

        public List<GoodsItem> AvailableGoodsItems
        {
            get
            {
                return _availableGoods;
            }
            set { _availableGoods = value; }
        }

        public List<Offer> AvailableOffers
        {
            get { return _availableOffers; }
            set { _availableOffers = value; }
        }
    }
}
