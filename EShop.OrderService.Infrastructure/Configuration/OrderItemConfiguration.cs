using EShop.OrderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Infrastructure.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            // Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Foreign Key
            builder.Property(x => x.OrderId)
                   .IsRequired();

            builder.Property(x => x.ProductId)
                   .IsRequired();

            // Quantity
            builder.Property(x => x.Quantity)
                   .IsRequired()
                   .HasDefaultValue(1);

            // Unit Price (money precision)
            builder.Property(x => x.UnitPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // TotalPrice is a computed property in C#, not mapped to DB
            builder.Ignore(x => x.TotalPrice);

            // Relationship: Order → OrderItems (1-to-many)
            builder.HasOne(x => x.Order)
                   .WithMany(o => o.Items)
                   .HasForeignKey(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
