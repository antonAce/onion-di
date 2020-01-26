namespace OnionDI.Domain.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string ProductGtin { get; set; }
        public Product Product { get; set; }

        public int ProductCount { get; set; }
    }
}