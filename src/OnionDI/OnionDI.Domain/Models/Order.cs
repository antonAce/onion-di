using System;
using System.Collections.Generic;

namespace OnionDI.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderingDate { get; set; }
    }
}