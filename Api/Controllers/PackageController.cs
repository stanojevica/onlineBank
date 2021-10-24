using Application;
using Application.DataTransfer.Packages;
using Application.Searches;
using Implementation.Commands.Packages;
using Implementation.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {

        private readonly UseCaseExecutor _executor;

        public PackageController(UseCaseExecutor executor)
        {
            _executor = executor;
        }
        // GET: api/<PackageController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchPackage search,
            [FromServices] GetPackageQuery query)
        {
            try
            {
                return Ok(_executor.ExecuteQuery(query, search));
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        // GET api/<PackageController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PackageController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] PackageDto dto,
            [FromServices] CreatePackageCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Uspešno dodat bankovni paket");
        }

        // PUT api/<PackageController>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] PackageDto dto,
            [FromServices] UpdatePackageCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Uspešno izmenjeni podaci o bankovnom paketu");
        }

        // DELETE api/<PackageController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] DeletePackageCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return StatusCode(201, "Uspešno obrisani podaci o bankovnom paketu");
        }

        // DELETE api/<PackageController>/Deactivate/5
        [HttpDelete("deactivate/{id}")]
        [Authorize]
        public IActionResult Deaktivacija(int id, [FromServices] DeactivatePackageCommand command)
        {
        
            _executor.ExecuteCommand(command, id);
            return StatusCode(201, "Uspešno deaktiviran bankovni paket");
        }
    }
}
