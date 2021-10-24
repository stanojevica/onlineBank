using Application;
using Application.DataTransfer.Users;
using Application.Interfaces;
using Application.Searches;
using DataAccess;
using Implementation.Commands.Users;
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
    public class UserController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;
        private readonly IApplicationUser _user;
        private readonly Context _context;

        public UserController(UseCaseExecutor executor, IApplicationUser user, Context context)
        {
            _executor = executor;
            _user = user;
            _context = context;
        }

        // GET: api/<UserController>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] SearchUser search,
            [FromServices] GetUserQuery query)
        {
            var user = _context.Users.Find(_user.Id);
            if(user.RoleId > 3)
            {
                search.Id = user.Id;
            }
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] UserDto dto,
            [FromServices] CreateUserCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Uspešno dodat korisnik");
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [Authorize]

        public IActionResult Put(int id, [FromBody] UserDto dto,
            [FromServices] UpdateUserCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Uspešno izmenjeni podaci o korisniku");
        }

        // PUT api/<UserController>/password/5
        [HttpPut("password/{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] UserDto dto,
            [FromServices] UpdatePasswordCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Uspešno izmenjen password");
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] DeleteUserCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return StatusCode(201, "Uspešno obrisani podaci o korisniku");
        }

        // DELETE api/<UserController>/deactivate/5
        [HttpDelete("deactivate/{id}")]
        public IActionResult Deaktivacija(int id, [FromServices] DeactivateUserCommand command)
        {

            _executor.ExecuteCommand(command, id);
            return StatusCode(201, "Uspešno deaktiviran korisnik");
        }


    }
}
