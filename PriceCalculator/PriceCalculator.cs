using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PriceCalculator.Models;

namespace PriceCalculator
{
    public class PriceCalculator
    {
        private List<Offer> _availableOffers;
        private List<GoodsItem> _availableGoods;

        public PriceCalculator():this(new PriceCalculatorConfiguration())
        {
            var configuration = new PriceCalculatorConfiguration();
            _availableGoods = configuration.AvailableGoodsItems;
            _availableOffers = configuration.AvailableOffers;
        }

        public PriceCalculator(PriceCalculatorConfiguration configuration)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-GB");
            this._availableGoods = configuration.AvailableGoodsItems;
            this._availableOffers = configuration.AvailableOffers;
        }

        public Basket GetCompletedBasket(string[] selectedGoodsArgs)
        {
            var selectedGoods = GetSelectedGoods(selectedGoodsArgs);
            var basket = new Basket(BuildBasket(selectedGoods));
            basket.FinaliseBasket();
            return basket;
        }

        private List<GoodsItem> GetSelectedGoods(string[] selectedGoodsArgs)
        {
            var selectedGoods = new List<GoodsItem>();
            foreach (var argument in selectedGoodsArgs)
            {
                var goodsItem = _availableGoods.SingleOrDefault(c =>
                    c.Name.Equals(argument.Trim(), StringComparison.InvariantCultureIgnoreCase));
                if (goodsItem != null)
                {
                    selectedGoods.Add(goodsItem);
                }
                else
                {
                    throw new ArgumentException($"Unable to find item name {argument}, available goods are {string.Join(",", _availableGoods.Select(c => c.Name).ToList())}");
                }
            }
            return selectedGoods;
        }

        private List<BasketItem> BuildBasket(List<GoodsItem> selectedGoods)
        {
            var basketItems = new List<BasketItem>();
            foreach (var item in selectedGoods)
            {
                var offer = _availableOffers.SingleOrDefault(c => c.GoodsItemName == item.Name);
                if (offer == null)
                {
                    basketItems.Add(new BasketItem(item));
                }
                else
                {
                    basketItems.Add(new BasketItem(item, offer.Discount));
                }
            }
            return basketItems;
        }
    }
}
