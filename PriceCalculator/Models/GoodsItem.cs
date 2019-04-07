namespace PriceCalculator.Models
{
    public class GoodsItem
    {
        public GoodsItem(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        public string Name { get;  }
        public decimal Price { get; }
    }
}
