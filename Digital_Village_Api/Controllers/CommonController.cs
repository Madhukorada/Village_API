using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Digital_Village_Api.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Digital_Village_Api.Controllers
{
    [Route("api/[controller]")]
    public class CommonController : Controller
    {
        private readonly ILogger<CommonController> _logger;
        private readonly ICommonRepository _commonRepository;

        public CommonController(ILogger<CommonController> logger,ICommonRepository commonRepository)
        {
            _logger = logger;
            _commonRepository=commonRepository;
        }

        [HttpGet]
        [Route("States")]
        public async Task<ActionResult> GetState()
        {
           var states = await _commonRepository.GetStates();
 
            return Ok(states);
        }
         [HttpGet]
        [Route("Districts")]
         public async Task<ActionResult> GetDistricts(int id)
        {
           var districts = await _commonRepository.GetDistricts(id);
 
            return Ok(districts);
        }
         [HttpGet]
        [Route("Subdistricts")]
         public async Task<ActionResult> GetSubDistrict(int id)
        {
           var subdistricts = await _commonRepository.GetSubDistrict(id);
 
            return Ok(subdistricts);
        }
    }
}