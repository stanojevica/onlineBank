using Application;
using Application.DataTransfer.Credits;
using Application.Searches;
using Implementation.Commands.Credits;
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
    public class CreditController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public CreditController(UseCaseExecutor executor)
        {
            _executor = executor;
        }
        // GET: api/<CreditController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchCredit search,
            [FromServices] GetCreditQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<CreditController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CreditController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreditDto dto,
            [FromServices] CreateCreditCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Uspešno dodat kredit");
        }

        // PUT api/<CreditController>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] CreditDto dto,
            [FromServices] UpdateCreditCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return StatusCode(204, "Izmenjeni podaci o kreditu");
        }

        // DELETE api/<CreditController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] DeleteCreditCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return StatusCode(201, "Uspešno obrisani podaci o kreditu");
        }

        // DELETE api/<CreditController>/deactivate/5
        [HttpDelete("deactivate/{id}")]
        [Authorize]
        public IActionResult Deaktivacija(int id, [FromServices] DeactivateCreditCommand command)
        {

            _executor.ExecuteCommand(command, id);
            return StatusCode(201, "Uspešno deaktiviran kredit");
        }
    }
}
