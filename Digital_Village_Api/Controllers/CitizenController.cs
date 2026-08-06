using Digital_village_Api.Infrastructure.ExternalServices;
using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Digital_Village_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        public readonly ICitizenService CitizenService;
        public CitizenController(ICitizenService _CitizenService)
        {
            CitizenService = _CitizenService;
        }
        [HttpPost]
        [Route("AddCitizen")]
        public Response AddCitizen(Citizen CT)
        {
            var Added = CitizenService.AddCitizen(CT);
            if (Added)
            {
                return new Response
                {
                    Statuscode = 200,
                    Message = $"your registration done Successfully " + CT.FirstName + " " + CT.LastName,
                    Data = null,

                };
            }

            else
            {
                return new Response
                {
                    Statuscode = 400,
                    Message = "Failed to register citizen",
                    Data = null
                };


            }
        }
        [HttpGet]
        [Route("GetCitizen")]
        public ActionResult<List<Citizen>> GetCitizen()
        {
            var list = CitizenService.GetCitizens();
            return Ok(list);
        }

    }
}
