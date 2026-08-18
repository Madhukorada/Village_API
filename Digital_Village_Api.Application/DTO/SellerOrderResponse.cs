using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Village_Api.Application.DTO
{
    public class SellerOrderResponse
    {
         public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; }

    public string UserName { get; set; }
    public string Mobile { get; set; }
    public string Address { get; set; }

    public List<SellerOrderItemResponse> Items { get; set; }
        = new List<SellerOrderItemResponse>();
    }
}