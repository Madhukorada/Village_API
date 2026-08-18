using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Village_Api.Application.DTO
{
    public class CustomerOrderResponse
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }

        public List<CustomerOrderItemResponse> Items { get; set; }
            = new List<CustomerOrderItemResponse>();
    }
}
