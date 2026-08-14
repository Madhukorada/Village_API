using System;
using System.Collections.Generic;

namespace Digitial_Village_Api.Domain.Persistence.Entities;

public partial class ViOrder
{
    public int OrderId { get; set; }

    public int RegistrationId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string OrderStatus { get; set; } = null!;

    public virtual ViRegistration Registration { get; set; } = null!;

    public virtual ICollection<ViOrderDetail> ViOrderDetails { get; set; } = new List<ViOrderDetail>();
}
