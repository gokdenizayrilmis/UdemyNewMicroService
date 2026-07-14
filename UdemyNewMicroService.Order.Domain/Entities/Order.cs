using MassTransit;
using System;
using System.Collections.Generic;
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
        public Guid PaymentId { get; set; }
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

        public static Order CreateUnPaid(Guid buyerId, int addressId, float? discountRate)
        {
            return new Order
            {
                Id = NewId.NextGuid(),
                Code = GenerateCode(),
                BuyerId = buyerId,
                Created = DateTime.UtcNow,
                Status = OrderStatus.WaitingForPayment,
                AddressId = addressId,
                TotalPrice = 0, // This would be calculated based on order items and discount
                PaymentId = Guid.Empty
            };
        }

        public void AddOrderItem(Guid productId, string productname, decimal unitPrice)
        {
            var orderItem = new OrderItem();
            orderItem.SetItem(productId, productname, unitPrice);
            OrderItems.Add(orderItem);
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = OrderItems.Sum(x => x.UnitPrice);
            if(DiscountRate.HasValue)
            {
                TotalPrice -= TotalPrice * (decimal)DiscountRate.Value / 100;
            }
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
