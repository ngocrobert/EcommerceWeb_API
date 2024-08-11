namespace Product.Core.Entities
{
    public class OrderItems
    {
        public OrderItems()
        {

        }

        public OrderItems(ProductItemOrdered productItemOrdered, decimal price, decimal quantity)
        {
            ProductItemOrdered = productItemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public int OrderItemsId { get; set; }
        public ProductItemOrdered ProductItemOrdered { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}