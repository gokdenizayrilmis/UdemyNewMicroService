namespace UdemyNewMicroService.Basket.Api.Data
{
    public class BasketItem
    {
        public BasketItem()
        {
            
        }
        public BasketItem(Guid ıd, string name, string? ımageUrl, decimal price, decimal? priceByApplyDiscountRate)
        {
            Id = ıd;
            Name = name;
            ImageUrl = ımageUrl;
            Price = price;
            PriceByApplyDiscountRate = priceByApplyDiscountRate;
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceByApplyDiscountRate { get; set; }

      
    }
}
