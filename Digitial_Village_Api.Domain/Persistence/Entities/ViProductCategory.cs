using System;
using System.Collections.Generic;

namespace Digitial_Village_Api.Domain.Persistence.Entities;

public partial class ViProductCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<ViProduct> ViProducts { get; set; } = new List<ViProduct>();
}
