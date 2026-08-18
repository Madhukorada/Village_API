using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Digital_Village_Api.Application.DTO;
using Digital_Village_Api.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Digital_Village_Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository, ILogger<OrderController> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [Route("PlaceOrder")]
        public async Task<ActionResult> PlaceOrder([FromBody] PlaceOrderRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new
                    {
                        Message = "Invalid order request."
                    });
                }

                if (request.RegistrationId <= 0)
                {
                    return BadRequest(new
                    {
                        Message = "Invalid registration id."
                    });
                }

                if (request.Items == null ||
                    request.Items.Count == 0)
                {
                    return BadRequest(new
                    {
                        Message = "Cart is empty."
                    });
                }

                var result =
                    await _orderRepository.PlaceOrder(request);

                if (result == "Order placed successfully")
                {
                    return Ok(new
                    {
                        Message = result
                    });
                }

                return BadRequest(new
                {
                    Message = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }
        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("CustomerOrders")]
        public async Task<ActionResult> GetCustomerOrders(int registrationId)
        {
            try
            {
                var orders =
                    await _orderRepository.GetCustomerOrdersAsync(registrationId);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Unable to get customer orders.",
                    error = ex.Message
                });
            }
        }
        [Authorize(Roles = "Seller")]
        [HttpGet]
        [Route("SellerOrders")]
        public async Task<ActionResult> GetSellerOrders(int registrationId)
        {
            try
            {
                var orders =
                    await _orderRepository
                        .GetSellerOrdersAsync(registrationId);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Unable to get seller orders.",
                    error = ex.Message
                });
            }
        }
        [Authorize(Roles = "Seller")]
        [HttpPut]
        [Route("UpdateOrders")]
        public async Task<ActionResult> UpdateOrder(int orderid, string orderstatus)
        {
            try
            {
                var result =
                    await _orderRepository
                        .UpdateOrder(orderid, orderstatus);

                return Ok(new
                {
                    Message = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Unable to update the order.",
                    error = ex.Message
                });
            }
        }
    }
}