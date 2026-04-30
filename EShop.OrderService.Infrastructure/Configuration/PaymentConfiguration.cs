using EShop.OrderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Infrastructure.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            // Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Foreign Key
            builder.Property(x => x.OrderId)
                   .IsRequired();

            // Payment Method
            builder.Property(x => x.PaymentMethod)
                   .HasMaxLength(50)
                   .IsRequired();

            // Payment Status
            builder.Property(x => x.PaymentStatus)
                   .HasMaxLength(50)
                   .IsRequired();

            // Transaction ID (from Stripe/PayPal/etc.)
            builder.Property(x => x.TransactionId)
                   .HasMaxLength(200)
                   .IsRequired(false);

            // Payment Date
            builder.Property(x => x.PaymentDate)
                   .IsRequired();

            // Relationship: Order → Payment (1-to-1)
            builder.HasOne(x => x.Order)
                   .WithOne(o => o.Payment)
                   .HasForeignKey<Payment>(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
