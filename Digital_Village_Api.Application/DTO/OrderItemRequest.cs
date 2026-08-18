using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Village_Api.Application.DTO
{
    public class OrderItemRequest
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}