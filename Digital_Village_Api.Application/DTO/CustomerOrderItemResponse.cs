using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Village_Api.Application.DTO
{
    public class CustomerOrderItemResponse
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public string? ProductImageUrl { get; set; }
    }
}