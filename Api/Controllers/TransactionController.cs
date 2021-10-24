using Application;
using Application.DataTransfer.Users.Transactions;
using Application.Interfaces;
using Application.Searches;
using DataAccess;
using Implementation.Commands.Users.Transactions;
using Implementation.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;
        private readonly IApplicationUser _user;
        private readonly Context _context;
        public TransactionController(UseCaseExecutor executor, IApplicationUser user, Context context)
        {
            _executor = executor;
            _user = user;
            _context = context;
        }
        
        // GET: api/<TransactionController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchTransaction search,
            [FromServices] GetTransactionQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // POST api/<TransactionController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] TransactionDto dto,
            [FromServices] TransactionCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Uspešno uzvršena transakcija.");
        }

        // PUT api/<TransactionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TransactionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
