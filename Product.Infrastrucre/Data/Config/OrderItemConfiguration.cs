﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.OwnsOne(x => x.ProductItemOrdered, n => { n.WithOwner(); });
            builder.Property(x => x.Price).HasColumnType("decimal(18,2");
            builder.Property(x => x.Quantity)
           .HasColumnType("decimal(18,2)");
        }
    }
}
