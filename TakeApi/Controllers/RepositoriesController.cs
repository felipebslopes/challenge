using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TakeApi.Data;
using TakeApi.Dtos;
using TakeApi.Model;


namespace TakeApi.Controllers
{
    [ApiController]
    [Route("api/v{verson:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class RepositoriesController : ControllerBase
    {
        private IService _service;  

        

        public RepositoriesController(IService service)
        {
            
            _service = service;
        }
        /// <summary>
        /// Método responsável por retornar os repositórios filtrados de acordo com a regra do desafio
        /// </summary>
        /// <returns></returns>
        // GET: api/<RepositoriesControllers>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var challenges = await _service.GetRepositories();
                var count = 0;
                foreach(var item in challenges)
                {
                    item.id = count;
                    count++;
                }
                var json = JsonConvert.SerializeObject(challenges, Formatting.Indented);

                return Ok(json);
            }
            catch(Exception )
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
