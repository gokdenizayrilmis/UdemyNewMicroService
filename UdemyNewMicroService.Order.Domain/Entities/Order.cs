using System.Text;

namespace UdemyNewMicroService.Order.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public string Code { get; set; } = null!;
        public DateTime Created { get; set; }
        public Guid BuyerId { get; set; }
        public OrderStatus Status { get; set; }
        public int AddressId { get; set; }
        public decimal TotalPrice { get; set; }
        public float? DiscountRate { get; set; }
        public Guid? PaymentId { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public Address Address { get; set; }

        public static string GenerateCode()
        {
            var random = new Random();
            var orderCode = new StringBuilder(10);

            for (int i = 0; i < 10; i++)
            {
                orderCode.Append(random.Next(0, 10));
            }

            return orderCode.ToString();
        }

        public static Order CreateUnPaidOrder(Guid buyerId, float? discountRate, int addressId)
        {
            return new Order
            {
                Id = Guid.CreateVersion7(),
                Code = GenerateCode(),
                BuyerId = buyerId,
                Created = DateTime.UtcNow,
                Status = OrderStatus.WaitingForPayment,
                AddressId = addressId,
                TotalPrice = 0, // This would be calculated based on order items and discount
                PaymentId = Guid.Empty
            };
        }

        public static Order CreateUnPaidOrder(Guid buyerId, float? discountRate)
        {
            return new Order
            {
                Id = Guid.CreateVersion7(),
                Code = GenerateCode(),
                BuyerId = buyerId,
                Created = DateTime.UtcNow,
                Status = OrderStatus.WaitingForPayment,
                TotalPrice = 0, // This would be calculated based on order items and discount
                PaymentId = Guid.Empty
            };
        }

        public void AddOrderItem(Guid productId, string productname, decimal unitPrice)
        {
            var orderItem = new OrderItem();

            if (DiscountRate.HasValue)
            {
                unitPrice -= unitPrice * (decimal)DiscountRate.Value / 100;
            }

            orderItem.SetItem(productId, productname, unitPrice);
            OrderItems.Add(orderItem);

            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = OrderItems.Sum(x => x.UnitPrice);
        }

        public void ApplyDiscount(float discountRate)
        {
            DiscountRate = discountRate;
            CalculateTotalPrice();
        }

        public void SetPaymentId(Guid paymentId)
        {
            Status = OrderStatus.Paid;
            this.PaymentId = paymentId;
        }

    }
}
