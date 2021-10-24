using Application;
using Application.DataTransfer.Users.Credits;
using Application.Interfaces;
using Implementation.Commands.Users.Credits;
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
    public class CreditUserController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;
        private readonly IApplicationUser _user;

        public CreditUserController(UseCaseExecutor executor, IApplicationUser user)
        {
            _executor = executor;
            _user = user;
        }


        // GET: api/<CrediUserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CrediUserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CrediUserController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreditUserDto dto,
            [FromServices] RequestCreditCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Uspešno ste podneli zahtev za kredit");
        }

        // PUT api/<CrediUserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreditStatusDto dto,
            [FromServices] UpdateCreditStatusCommand command)
        {
            dto.UserId = id;
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Uspešno ste izmenili status kredita");
        }

        // DELETE api/<CrediUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
