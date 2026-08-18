using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Village_Api.Application.DTO
{
   
    
    public class PlaceOrderRequest
    {
        public int RegistrationId { get; set; }

        public List<OrderItemRequest> Items { get; set; } = new();
    }
    
}