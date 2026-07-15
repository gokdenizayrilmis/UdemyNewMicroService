using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNewMicroService.Order.Domain.Entities;

namespace UdemyNewMicroService.Order.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Province).IsRequired().HasMaxLength(50);
            builder.Property(x => x.District).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Street).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ZipCode).IsRequired().HasMaxLength(10);
        }
    }
}
