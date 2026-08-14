using System;
using System.Collections.Generic;

namespace Digitial_Village_Api.Domain.Persistence.Entities;

public partial class ViRegistration
{
    public int RegistrationId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string State { get; set; } = null!;

    public string District { get; set; } = null!;

    public string Subdistrict { get; set; } = null!;

    public string VillageName { get; set; } = null!;

    public string Pincode { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? ShopName { get; set; }

    public string? ShopImage { get; set; }

    public string? ShopGovtRegistrationId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<ViOrder> ViOrders { get; set; } = new List<ViOrder>();

    public virtual ICollection<ViProduct> ViProducts { get; set; } = new List<ViProduct>();
}
