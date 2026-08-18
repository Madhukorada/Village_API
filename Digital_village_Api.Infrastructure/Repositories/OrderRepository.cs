using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digital_Village_Api.Application.DTO;
using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Persistence;
using Digitial_Village_Api.Domain.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Digital_village_Api.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly VillageDbContext _villageDbContext;
        public OrderRepository(VillageDbContext villageDbContext)
        {
            _villageDbContext = villageDbContext;
        }

        public async Task<string> PlaceOrder(PlaceOrderRequest request)
        {

            using var transaction =
           await _villageDbContext.Database.BeginTransactionAsync();

            try
            {
                if (request.Items == null ||
                    request.Items.Count == 0)
                {
                    return "Cart is empty";
                }

                decimal totalAmount = 0;

                // Create Order
                var order = new ViOrder
                {
                    RegistrationId = request.RegistrationId,
                    OrderDate = DateTime.Now,
                    TotalAmount = 0,
                    OrderStatus = "Pending"
                };

                _villageDbContext.ViOrders.Add(order);

                await _villageDbContext.SaveChangesAsync();

                // Create Order Details
                foreach (var item in request.Items)
                {
                    if (item.Quantity <= 0)
                    {
                        await transaction.RollbackAsync();
                        return "Invalid quantity";
                    }

                    var product = await _villageDbContext.ViProducts.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);

                    if (product == null)
                    {
                        await transaction.RollbackAsync();

                        return $"Product {item.ProductId} not found";
                    }

                    decimal unitPrice = product.ProductPrice;

                    decimal discountAmount = 0;

                    if (product.ProductDiscount.HasValue)
                    {
                        discountAmount =
                            unitPrice *
                            product.ProductDiscount.Value / 100;
                    }

                    decimal priceAfterDiscount =
                        unitPrice - discountAmount;

                    decimal itemTotal =
                        priceAfterDiscount * item.Quantity;

                    totalAmount += itemTotal;

                    var orderDetail = new ViOrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = unitPrice,
                        DiscountAmount = discountAmount,
                        TotalPrice = itemTotal
                    };

                    _villageDbContext.ViOrderDetails.Add(orderDetail);
                }

                // Update order total
                order.TotalAmount = totalAmount;

                await _villageDbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return "Order placed successfully";
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }
        public async Task<List<CustomerOrderResponse>> GetCustomerOrdersAsync(
       int registrationId)
        {
            var orders = await _villageDbContext.ViOrders
                .Where(x => x.RegistrationId == registrationId)

                .Include(x => x.ViOrderDetails)
                .ThenInclude(d => d.Product)
                .OrderByDescending(x => x.OrderDate)
                .Select(x => new CustomerOrderResponse
                {
                    OrderId = x.OrderId,
                    OrderDate = x.OrderDate,
                    TotalAmount = x.TotalAmount,
                    OrderStatus = x.OrderStatus,

                    Items = x.ViOrderDetails
                        .Select(d => new CustomerOrderItemResponse
                        {
                            ProductName = d.Product.ProductName,
                            Quantity = d.Quantity,
                            UnitPrice = d.UnitPrice,
                            DiscountAmount = d.DiscountAmount,
                            TotalPrice = d.TotalPrice,
                            ProductImageUrl = d.Product.ProductImageUrl
                        })
                        .ToList()
                })
                .ToListAsync();

            return orders;
        }

        public async Task<List<SellerOrderResponse>> GetSellerOrdersAsync(
            int registrationId)
        {
            var orders = await _villageDbContext.ViOrders

                // OrderDetails
                .Include(o => o.ViOrderDetails)

                // Product inside OrderDetails
                .ThenInclude(d => d.Product)

                // Customer information
                .Include(o => o.Registration)

                .Where(o =>
                    o.ViOrderDetails.Any(d =>
                        d.Product.RegistrationId == registrationId
                    )
                )

                .OrderByDescending(o => o.OrderDate)

                .Select(o => new SellerOrderResponse
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    OrderStatus = o.OrderStatus,

                    UserName =
                        o.Registration.FirstName + " " +
                        o.Registration.LastName,

                    Mobile = o.Registration.Mobile,

                    Address = o.Registration.Address,

                    Items = o.ViOrderDetails

                        .Where(d =>
                            d.Product.RegistrationId == registrationId
                        )

                        .Select(d => new SellerOrderItemResponse
                        {
                            ProductName =
                                d.Product.ProductName,

                            Quantity =
                                d.Quantity,

                            UnitPrice =
                                d.UnitPrice,

                            DiscountAmount =
                                d.DiscountAmount,

                            TotalPrice =
                                d.TotalPrice,

                            ProductImageUrl =
                                d.Product.ProductImageUrl,

                            IsActive =
                                d.Product.IsActive
                        })

                        .ToList()
                })

                .ToListAsync();

            return orders;
        }

        public async Task<string> UpdateOrder(int orderid,string orderstatus)
        {
            var order= await _villageDbContext.ViOrders.FirstOrDefaultAsync(x=>x.OrderId==orderid);
             if (order == null)
            {
                return "order not found";
            }

            order.OrderStatus=orderstatus;
            await _villageDbContext.SaveChangesAsync();
            return "order Updated";
        }
    }
}