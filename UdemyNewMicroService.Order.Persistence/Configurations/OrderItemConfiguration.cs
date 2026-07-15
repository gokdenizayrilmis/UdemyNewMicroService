using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNewMicroService.Order.Domain.Entities;

namespace UdemyNewMicroService.Order.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).UseIdentityColumn();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Productname).IsRequired().HasMaxLength(200);
            builder.Property(x => x.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
        }
    }
}
