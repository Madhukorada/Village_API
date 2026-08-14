using System;
using System.Collections.Generic;

namespace Digitial_Village_Api.Domain.Persistence.Entities;

public partial class ViSubDistrict
{
    public int SubDistrictId { get; set; }

    public string SubDistrictName { get; set; } = null!;

    public int DistrictId { get; set; }

    public virtual ViDistrict District { get; set; } = null!;
}
