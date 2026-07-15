using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNewMicroService.Order.Domain.Entities;
using UdemyNewMicroService.Order.Persistence.Configurations;

namespace UdemyNewMicroService.Order.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        DbSet<Domain.Entities.Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Address> Addresses { get; set; }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceAssembly).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
