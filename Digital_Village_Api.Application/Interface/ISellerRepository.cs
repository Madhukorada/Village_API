
using System;
using System.Collections.Generic;
using System.Text;
using Digitial_Village_Api.Domain.Entities;
using Digitial_Village_Api.Domain.Persistence.Entities;


namespace Digital_Village_Api.Application.Interface
{
    public interface ISellerRepository
    {

        public  Task<string> RegisterSeller(ViRegistration vr);
    }
}
