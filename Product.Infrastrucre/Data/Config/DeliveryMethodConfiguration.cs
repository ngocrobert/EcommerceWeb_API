using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Data.Config
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.HasData(
                
                new DeliveryMethod { ID=1, ShortName="GHTK", Description="Fastest", DeliveryTime = "", Price = 50},
               
                new DeliveryMethod { ID = 2, ShortName = "ShopeeExpress", Description = "Get it with 3 days", DeliveryTime = "", Price =30},
                new DeliveryMethod { ID = 3, ShortName = "ViettelPost", Description = "Slower", DeliveryTime = "", Price =20},
              
                new DeliveryMethod { ID = 4, ShortName = "VnPost", Description = "Free", DeliveryTime="", Price =0}

                );
        }
    }
}
