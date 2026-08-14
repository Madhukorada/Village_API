using System;
using System.Collections.Generic;

namespace Digitial_Village_Api.Domain.Persistence.Entities;

public partial class ViProduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int ProductQuantity { get; set; }

    public decimal ProductPrice { get; set; }

    public int? ProductDiscount { get; set; }

    public int RegistrationId { get; set; }

    public string? ProductImageUrl { get; set; }

    public string? ProductCategory { get; set; }

    public virtual ViRegistration Registration { get; set; } = null!;

    public virtual ICollection<ViOrderDetail> ViOrderDetails { get; set; } = new List<ViOrderDetail>();

}
