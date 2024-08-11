using Microsoft.EntityFrameworkCore;
using Product.Core;
using Product.Core.Entities;
using Product.Core.Services;
using Product.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repository
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _uOW;
        private readonly ApplicationDbContext _context;

        public OrderServices(IUnitOfWork UOW, ApplicationDbContext context)
        {
            _uOW = UOW;
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShipAddress shipAddress)
        {
           //Get Basket Items
           var basket = await _uOW.BasketRepository.GetBasketAsync(basketId);
            var items = new List<OrderItems>();
            foreach(var item in basket.BasketItems)
            {
                var productItem = await _uOW.ProductRepository.GetByIdAsync(item.Id);
                var productItemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.ProductPicture);
                var OrderItem = new OrderItems(productItemOrdered, item.Price, item.Quantity);
                items.Add(OrderItem);
            }
            await _context.OrderItems.AddRangeAsync(items);
            await _context.SaveChangesAsync();

            //Delivery Method
            var deliveryMethod = await _context.DeliveryMethods.Where(x => x.ID == deliveryMethodId).FirstOrDefaultAsync();
            //Calculate subtotal
            var subtotal = items.Sum(x => x.Price * x.Quantity);
            //check order null
            var order = new Order(buyerEmail, shipAddress, deliveryMethod, items, subtotal);
            if (order is null) return null;
            //adding order DB 
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            //Remove basket after save in db order
            await _uOW.BasketRepository.DeleteBasketAsync(basketId);

            return order;

        } 

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _context.DeliveryMethods.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var order = await _context.Orders
                .Where(x => x.OrderId == id && x.BuyerEmail == buyerEmail)
                .Include(x => x.OrderItems)
                .Include(x => x.DeliveryMethod).FirstOrDefaultAsync();
            return order;

        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var order = await _context.Orders
                .Where(x => x.BuyerEmail == buyerEmail)
                .Include(x => x.OrderItems).ThenInclude(x => x.ProductItemOrdered)
                .Include(x => x.DeliveryMethod)
                .OrderByDescending(x => x.OrderDate).ToListAsync();
            return order;
        }
    }
}
