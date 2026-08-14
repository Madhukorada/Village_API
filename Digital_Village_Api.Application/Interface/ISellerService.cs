using Digitial_Village_Api.Domain.Entities;
using Digitial_Village_Api.Domain.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_Village_Api.Application.Interface
{
    public interface ISellerService
    {

        public Task<string> RegisterSeller(ViRegistration vr);
    }

}
