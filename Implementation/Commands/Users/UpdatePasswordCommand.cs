using Application.DataTransfer.Users;
using Application.Interfaces;
using Application.Interfaces.Commands.UsersCommands;
using DataAccess;
using FluentValidation;
using Implementation.Validators.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Users
{
    public class UpdatePasswordCommand : IUpdatePasswordCommand
    {
        public int Id => 12;

        public string Name => "Izmena korisničkog pasworda";

        private readonly Context _context;
        private readonly PasswordValidator _validator;
        private readonly IApplicationUser _user;

        public UpdatePasswordCommand(Context context, PasswordValidator validator, IApplicationUser user)
        {
            _context = context;
            _validator = validator;
            _user = user;
        }

        public void Execute(UserDto request)
        {
            var user = _context.Users.Find(_user.Id);
            if (user == null)
            {
                throw new Exception("Korisnik ne postoji");
            }

            _validator.ValidateAndThrow(request);

            user.Password = request.Password;
            _context.SaveChanges();
        }
    }
}
