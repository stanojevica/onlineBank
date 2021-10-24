using Api.Core.Jwt;
using DataAccess;
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
    public class TokenController : ControllerBase
    {
        private readonly JwtManager _manager;
        private readonly Context _context;

        public TokenController(JwtManager manager, Context context)
        {
            _manager = manager;
            _context = context;
        }

        // POST api/<TokenController>
        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password && x.DeleteAt == null);

            if (user == null)
            {
                return UnprocessableEntity("Korisnik sa tim emailom i lozinkom ne postoji.");
            }

            var token = _manager.MakeToken(request.Email, request.Password);

            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(new
            {
                token
            });

        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
