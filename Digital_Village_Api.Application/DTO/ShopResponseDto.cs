using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_Village_Api.Application.DTO
{
    public class ShopResponseDto
    {
    public int RegistrationId { get; set; }
    public string? ShopName { get; set; }
    public string? ShopImage { get; set; }
    public string? VillageName { get; set; }
    }
}