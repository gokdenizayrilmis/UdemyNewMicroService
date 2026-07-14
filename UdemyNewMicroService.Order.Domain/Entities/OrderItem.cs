using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyNewMicroService.Order.Domain.Entities
{
    // Anemic Domain Model => Rich Domain Model
    public class OrderItem : BaseEntity<int>
    {
        public Guid ProductId { get; set; }
        public string Productname { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public void SetItem(Guid productId, string productname, decimal unitPrice)
        {

            if(string.IsNullOrEmpty(productname))
            {
                throw new ArgumentNullException(nameof(productname), "ProductName cannot be empty!");
            }

            if (unitPrice <= 0)
            {
                throw new ArgumentNullException(nameof(unitPrice), "UnitPrice cannot be less than or equal to zero!");
            }

            ProductId = productId;
            Productname = productname;
            UnitPrice = unitPrice;
        }

        public void UpdatePrice(decimal price) 
        {
            if(price <= 0)
            {
                throw new ArgumentNullException("UnitPrice cannot be less than or equal to zero!");
            }
            UnitPrice = price;
        }

        public void ApplyDiscount(float discountPercentage)
        {
            if(discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentNullException("Discount percentage must be between 0 and 100!");
            }
            this.UnitPrice -= this.UnitPrice * (decimal)(discountPercentage / 100);
        }

        public bool IsSameItem(OrderItem otherItem)
        {
            return this.ProductId == otherItem.ProductId;
        }

    }
}
