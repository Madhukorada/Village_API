using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digitial_Village_Api.Domain.Persistence.Entities;

namespace Digital_Village_Api.Application.Interface
{
    public interface ICommonRepository
    {
        public Task<List<ViState>> GetStates();
        public Task<List<ViDistrict>> GetDistricts(int id);
        public Task<List<ViSubDistrict>> GetSubDistrict(int id);
    }
}