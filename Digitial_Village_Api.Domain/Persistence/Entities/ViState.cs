using System;
using System.Collections.Generic;

namespace Digitial_Village_Api.Domain.Persistence.Entities;

public partial class ViState
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual ICollection<ViDistrict> ViDistricts { get; set; } = new List<ViDistrict>();
}
