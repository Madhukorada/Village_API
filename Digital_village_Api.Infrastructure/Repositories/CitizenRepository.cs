using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_village_Api.Infrastructure.Repositories
{
    public class CitizenRepository : ICitizenRepository
    {
        public readonly IExcelService _IExcelservice;
        public CitizenRepository(IExcelService IExcelservice) {
            _IExcelservice = IExcelservice;
        }
        public bool Add(Citizen citizen)
        {
            try
            {
                var ct = new Citizen()
                {
                    FirstName = citizen.FirstName,
                    LastName = citizen.LastName,
                    Age = citizen.Age,
                    Gender = citizen.Gender,
                    Mobile = citizen.Mobile,
                    FamilyHead = citizen.FamilyHead,
                    Villagecode = citizen.Villagecode,
                    UserName = citizen.UserName,
                    Password = citizen.Password
                };
                var SaveInExcel = _IExcelservice.ExcelSave(ct);
                if (SaveInExcel)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
            catch (Exception ex) 
            {
                    return false;
            }



        }
        public List<Citizen> GetAllCitizens()
        {
            return new List<Citizen>();
        }
    }
}
