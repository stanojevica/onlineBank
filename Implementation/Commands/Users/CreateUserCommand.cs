using Application.DataTransfer.Users;
using Application.Email;
using Application.Interfaces;
using Application.Interfaces.Commands.UsersCommands;
using DataAccess;
using Domain.Etities;
using FluentValidation;
using Implementation.Validators.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Users
{
    public class CreateUserCommand : ICreateUserCommand
    {
        public int Id => 9;

        public string Name => "Kreiranje korisnika";

        private readonly Context _context;
        private readonly CreateUserValidator _validator;
        private readonly IApplicationUser _user;
        private readonly IEmailSender _sender;

        public CreateUserCommand(Context context, CreateUserValidator validator, IApplicationUser user, IEmailSender sender)
        {
            _context = context;
            _validator = validator;
            _user = user;
            _sender = sender;
        }

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);

            var pass = request.Email + DateTime.Now.ToFileTime();
            pass = pass.GetHashCode().ToString();
            /*pass = pass.Substring(1, 9);*/

            var newUser = new User
            {
                Name = request.FirstName,
                LastName = request.LastName,
                IdentityNumber = request.IdentificationNumber,
                Email = request.Email,
                Password = pass,
                RoleId = request.RoleId,
                Active = true,
                CreatedAt = DateTime.Now
            };
            _context.Users.Add(newUser);

            var userDatabase = _context.Users.Find(_user.Id);
            if (userDatabase.RoleId == 2)
            {
                var lastAddedAccount = _context.Accounts.OrderByDescending(x => x.CreatedAt).First();
                var lastAccountNumber = lastAddedAccount.AccountNumber;
                var arrayOfNumStr = lastAccountNumber.Split("-");
                List<int> arrayOfNumInt = new List<int>();
                for (int i = 0; i < arrayOfNumStr.Count(); i++)
                {
                    arrayOfNumInt.Add(Int32.Parse(arrayOfNumStr[i]));
                }

                for(int i = arrayOfNumInt.Count - 1; i>=0; i--)
                {
                    if(i == arrayOfNumInt.Count - 1)
                    {
                        if (arrayOfNumInt[i] < 9999)
                        {
                            arrayOfNumInt[i] += 1;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if(i != arrayOfNumInt.Count - 1)
                    {
                        if(arrayOfNumInt[i] < 9999)
                        {
                            arrayOfNumInt[i] += 1;
                            arrayOfNumInt[i + 1] = 0;
                            break;
                        }
                    }
                    else
                    {
                        throw new Exception("Ne moze se vise naloga napraviti");
                    }
                }
                
                var newAccountNumber = "";
                for (var i = 0; i < arrayOfNumInt.Count(); i++)
                {
                    if (i < 3)
                    {
                        newAccountNumber += arrayOfNumInt[i] + "-";
                    }
                    else
                    {
                        newAccountNumber += arrayOfNumInt[i];
                    }
                }

                var newAccount = new Account
                {
                    AccountNumber = newAccountNumber,
                    PackageId = request.PackageId,
                    User = newUser,
                    AvailableFunds = 0,
                    CreatedAt = DateTime.Now,
                    DeleteAt = null,
                    UpdateAt = null,
                    Active = true
                };

                _context.Accounts.Add(newAccount);
            }

            _sender.Send(new EmailSenderDto
            {
                Subject = "Kreiranje naloga",
                SendTo = request.Email,
                Content = "Vaš nalog je uspešno napravljen. Vaša lozinka je: "+pass
            });

            _context.SaveChanges();
        }
    }
}
