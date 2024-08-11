
using Product.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Services
{
    public interface IOrderServices
    {
        // Tạo Order
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShipAddress shipAddress);
        // Lấy thông tin Order theo User
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        //Task<IEnumerable<Order>> GetOrdersForUserAsync(string buyerEmail);
        // Lấy thông tin Order theo ID
        Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
        // Lấy thông tin Delivery
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
