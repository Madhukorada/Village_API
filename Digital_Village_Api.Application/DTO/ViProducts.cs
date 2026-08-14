using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Digital_Village_Api.Application.DTO
{
    public class ViProducts
    {
    public int ProductId { get; set; } 
    public string ProductName { get; set; } = null!;

    public int ProductQuantity { get; set; }

    public decimal ProductPrice { get; set; }

    public int? ProductDiscount { get; set; }

    public string RegistrationId { get; set; }

    public IFormFile? ProductImage { get; set; }

    public string? ProductCategory { get; set; }
    }
}