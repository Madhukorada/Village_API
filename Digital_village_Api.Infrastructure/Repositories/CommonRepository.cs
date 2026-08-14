using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Persistence;
using Digitial_Village_Api.Domain.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Digital_village_Api.Infrastructure.Repositories
{
    public class CommonRepository:ICommonRepository
    {
       public readonly VillageDbContext _villageDbContext;
       public CommonRepository(VillageDbContext villageDbContext)
        {
            _villageDbContext=villageDbContext;
        }



public async Task<List<ViState>> GetStates()
{
    return await _villageDbContext.ViStates.ToListAsync();
}
        public async Task<List<ViDistrict>> GetDistricts(int id)
        {
            return await _villageDbContext.ViDistricts
        .Where(d => d.StateId == id)
        .ToListAsync();
        }

       
        public async Task<List<ViSubDistrict>> GetSubDistrict(int id)
        {
            return await _villageDbContext.ViSubDistricts
        .Where(d => d.DistrictId == id)
        .ToListAsync();
        }
    }
}