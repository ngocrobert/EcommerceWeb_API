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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //Cấu hình shipToAddress là một thuộc tính thuộc kiểu sở hữu riêng biệt (Owned Entity). WithOwner() chỉ ra rằng thực thể này phụ thuộc vào thực thể Order và không thể tồn tại độc lập.
            builder.OwnsOne(x => x.ShipToAddress, n => { n.WithOwner(); });
            // Cấu hình thuộc tính orderStatus để ánh xạ enum OrderStatus sang chuỗi (string) khi lưu vào cơ sở dữ liệu, và ngược lại khi lấy ra từ cơ sở dữ liệu.
            builder.Property(x => x.OrderStatus).HasConversion(o => o.ToString(), o=>(OrderStatus)Enum.Parse(typeof(OrderStatus), o));
            //Xác định mối quan hệ giữa Order và OrderItems (một Order có thể chứa nhiều OrderItems).
            builder.HasMany(x => x.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            //Định nghĩa thuộc tính Subtotal có kiểu dữ liệu decimal với độ chính xác là 18 chữ số, trong đó có 2 chữ số thập phân
            builder.Property(x => x.Subtotal).HasColumnType("decimal(18,2)");
        }
    }
}
