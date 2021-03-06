using System.Collections.Generic;

namespace OnionDI.Domain.Models
{
    public class Product
    {
        public string Gtin { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}