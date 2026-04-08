using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_Village_Api.Application.Services
{
    public class CitizenService : ICitizenService
    {
        public readonly ICitizenRepository _citizenRepository;
        public CitizenService(ICitizenRepository citizenRepository)
        {
            _citizenRepository = citizenRepository;
        }
        public bool AddCitizen(Citizen citizen)
        {
            var addedCitizen = _citizenRepository.Add(citizen);
            if (addedCitizen)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
    }
}
