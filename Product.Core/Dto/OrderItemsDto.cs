namespace Product.Core.Dto
{
    public class OrderItemsDto
    {
        public int ProductItemId { get; set; }
        public string ProductItemName { get; set; }
        public string PictureUrl { get; set; }
        public decimal price { get; set; }
        public decimal quantity { get; set; }
    }
}