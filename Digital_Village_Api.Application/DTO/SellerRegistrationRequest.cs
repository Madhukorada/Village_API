using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_Village_Api.Application.DTO
{
    public class SellerRegistrationRequest
    {
        public Guid SellerId { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string ShopName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Subdistrict { get; set; } = string.Empty;
        public string VillageName { get; set; } = string.Empty;

        public required IFormFile ShopImage { get; set; } 
    }
}
