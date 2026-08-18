using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digital_Village_Api.Application.DTO;


namespace Digital_Village_Api.Application.Interface
{
    public interface IOrderRepository
    {
        Task<string> PlaceOrder(PlaceOrderRequest placeorderRequest);
        Task<List<CustomerOrderResponse>> GetCustomerOrdersAsync(int registrationId);
        Task<List<SellerOrderResponse>> GetSellerOrdersAsync(
        int registrationId);
         Task<string> UpdateOrder(int orderid,string orderstatus);
    }
}