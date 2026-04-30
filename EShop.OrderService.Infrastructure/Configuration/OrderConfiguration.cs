using EShop.OrderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace EShop.OrderService.Infrastructure.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
          
            builder.Property(o => o.OrderStatusId).IsRequired();
            builder.Property(o => o.CreatedAt).IsRequired();
            builder.Property(o => o.UpdatedAt);

            builder.HasMany(o => o.Items)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Payment)
                   .WithOne(p => p.Order)
                   .HasForeignKey<Payment>(p => p.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
           
        }
    }
}