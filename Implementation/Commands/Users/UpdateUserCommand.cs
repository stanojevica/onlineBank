using Application.DataTransfer.Users;
using Application.Interfaces;
using Application.Interfaces.Commands.UsersCommands;
using DataAccess;
using FluentValidation;
using Implementation.Validators.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Users
{
    public class UpdateUserCommand : IUpdateUserCommand
    {
        public int Id => 13;

        public string Name => "Izmena informacija o korisniku";

        private readonly Context _context;
        private readonly UpdateUserValidator _validator;
        private readonly IApplicationUser _user;

        public UpdateUserCommand(Context context, UpdateUserValidator validator, IApplicationUser user)
        {
            _context = context;
            _validator = validator;
            _user = user;
        }

        public void Execute(UserDto request)
        {
            var user = _context.Users
                .Include(x => x.Account)
                .FirstOrDefault(x => x.Id == request.Id);
            if (user == null)
            {
                throw new Exception("Korisnik nije pronadjen");
            }
            _validator.ValidateAndThrow(request);

            user.Name = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.IdentityNumber = request.IdentificationNumber;

            var userDatabase = _context.Users.Find(_user.Id);
            if (userDatabase.RoleId == 2)
            {
                user.RoleId = request.RoleId;
                user.Account.PackageId = request.PackageId;
            }

            _context.SaveChanges();
        }
    }
}
