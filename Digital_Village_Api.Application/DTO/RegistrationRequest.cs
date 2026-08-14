using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_Village_Api.Application.DTO
{

public class RegistrationRequest
{
   
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Mobile { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string Country { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string Subdistrict { get; set; } = string.Empty;

    public string VillageName { get; set; } = string.Empty;

    public string Pincode { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string? ShopName { get; set; }

    public  IFormFile? ShopImage { get; set; }

    public string? ShopGovtRegistrationId { get; set; }
}
}
