using EShop.OrderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace EShop.OrderService.Infrastructure.Configuration
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("OrderStatuses");

            // Primary Key (Auto Increment)
            builder.HasKey(x => x.OrderStatusId);

            builder.Property(x => x.OrderStatusId)
                   .ValueGeneratedOnAdd()              // Enables Identity (auto increment)
                   .UseIdentityColumn();               // Explicit identity configuration

            // Name
            builder.Property(x => x.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            // Relationship: OrderStatus → Orders (1-to-many)
            builder.HasMany(x => x.Orders)
                   .WithOne(o => o.OrderStatus)         
                   .HasForeignKey(o => o.OrderStatusId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Optional: Unique index on Name
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(
    new OrderStatus { OrderStatusId = 1, Name = "Pending" },
    new OrderStatus { OrderStatusId = 2, Name = "Paid" },
    new OrderStatus { OrderStatusId = 3, Name = "Shipped" },
    new OrderStatus { OrderStatusId = 7, Name = "Returned" },
    new OrderStatus {OrderStatusId=6, Name = "Refunded" },
    new OrderStatus { OrderStatusId = 4, Name = "Delivered" },
    new OrderStatus { OrderStatusId = 5, Name = "Cancelled" }
);

        }
    }
}
