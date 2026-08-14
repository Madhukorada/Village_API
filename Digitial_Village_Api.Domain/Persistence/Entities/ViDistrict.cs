using System;
using System.Collections.Generic;

namespace Digitial_Village_Api.Domain.Persistence.Entities;

public partial class ViDistrict
{
    public int DistrictId { get; set; }

    public string DistrictName { get; set; } = null!;

    public int StateId { get; set; }

    public virtual ViState State { get; set; } = null!;

    public virtual ICollection<ViSubDistrict> ViSubDistricts { get; set; } = new List<ViSubDistrict>();
}
