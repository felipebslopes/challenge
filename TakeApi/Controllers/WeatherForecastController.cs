using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TakeApi.Data;
using TakeApi.Dtos;
using TakeApi.Model;

namespace TakeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IService _service;  

        

        public WeatherForecastController(IService service)
        {
            
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var challenges = await _service.GetRepositories();

                return Ok(challenges);
            }
            catch(Exception )
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
