using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Entities
{
    public class Order
    {
        public Order()
        {

        }

        public Order(string buyerEmail, ShipAddress shipAddress, DeliveryMethod deliveryMethod, List<OrderItems> items, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = items;
            Subtotal = subtotal;
        }

        public Order(string buyerEmail, ShipAddress shipToAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItems> orderItems, decimal subtotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        //public Order(string buyerEmail, ShipAddress shipToAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItems> orderItems, decimal subtotal, string paymentIntentId)
        //{
        //    BuyerEmail = buyerEmail;
        //    ShipToAddress = shipToAddress;
        //    DeliveryMethod = deliveryMethod;
        //    OrderItems = orderItems;
        //    Subtotal = subtotal;
        //    PaymentIntentId = paymentIntentId;
        //}



        public int OrderId { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public ShipAddress ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItems> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.pending;

        public string? PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }

    }
}
