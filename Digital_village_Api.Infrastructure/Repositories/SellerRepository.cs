using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Entities;
using Digitial_Village_Api.Domain.Persistence;
using Digitial_Village_Api.Domain.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Digital_Village_Api.Application.DTO;

namespace Digital_village_Api.Infrastructure.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        public readonly IExcelService _IExcelservice;
        public readonly VillageDbContext _villageDbContext;
        public SellerRepository(IExcelService IExcelservice,VillageDbContext villageDbContext )
        {
            _IExcelservice = IExcelservice;
            _villageDbContext = villageDbContext;
        }
        public async Task<string> RegisterSeller(ViRegistration vr)
        {
            try
            {
                if (vr == null)
                {
                    return "No data received";
                }

                var vuser = new ViRegistration
                {
                    FirstName = vr.FirstName,
                    LastName = vr.LastName,
                    Mobile = vr.Mobile,
                    Password = vr.Password,
                    ConfirmPassword = vr.ConfirmPassword,
                    Gender = vr.Gender,
                    Email = vr.Email,
                    Country = vr.Country,
                    State = vr.State,
                    District = vr.District,
                    Subdistrict = vr.Subdistrict,
                    VillageName = vr.VillageName,
                    Pincode = vr.Pincode,
                    Address = vr.Address,
                    Role = vr.Role,
                    ShopName = vr.ShopName,
                    ShopImage = vr.ShopImage,
                    ShopGovtRegistrationId = vr.ShopGovtRegistrationId,
                    CreatedDate = DateTime.UtcNow
                };

                // Add record to DbContext
                 await _villageDbContext.ViRegistrations.AddAsync(vuser);

                // // Save record to SQL Server
                var dbResult = await _villageDbContext.SaveChangesAsync();

                if (dbResult <= 0)
                {
                    return "Registration failed in database";
                }

                // Save record to Excel
                var saveInExcel = _IExcelservice.ExcelSaves(
                    vuser,
                    "user.xlsx",
                    "user");

                if (!saveInExcel)
                {
                    return "Data saved in database, but Excel save failed";
                }

                return "Registration is successful";
            }
            catch (Exception ex)
            {
                return $"Registration failed: {ex.Message}";
            }
        }
         public async  Task<List<ShopResponseDto>> GetShops()
        {
            var result = await _villageDbContext.ViRegistrations.Where(x=>x.Role=="Seller")
            .Select(x => new ShopResponseDto
            {
                ShopImage=x.ShopImage,
                ShopName=x.ShopName,
                VillageName=x.VillageName,
                RegistrationId=x.RegistrationId
            }).ToListAsync();
            return result;
        }
    }

}




