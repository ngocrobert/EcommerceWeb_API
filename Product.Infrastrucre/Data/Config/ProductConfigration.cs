using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Infrastructure.Data.Config
{
    public class ProductConfigration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(128);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            

            //seeding
            builder.HasData(
                new Products { Id = 1, Name = "Product 1", Description = "description 4", Price = 100, CategoryId=1, ProductPicture = "https://" },
                new Products { Id = 2, Name = "Product 2", Description = "description 5", Price = 300, CategoryId = 2, ProductPicture = "https://" },
                new Products { Id = 3, Name = "Product 3", Description = "description 6", Price = 500, CategoryId = 1, ProductPicture = "https://" },
                new Products { Id = 4, Name = "Product 4", Description = "description 7", Price = 700, CategoryId = 3, ProductPicture = "https://" }

                );
        }
    }
}
