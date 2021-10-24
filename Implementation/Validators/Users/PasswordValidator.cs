using Application.DataTransfer.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Users
{
    public class PasswordValidator : AbstractValidator<UserDto>
    {
        public PasswordValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Pasvord je obaveyan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Password)
                    .Matches(@"^\d+$")
                    .MinimumLength(8)
                    .WithMessage("Pasvord mora imati minimum 8 cifara");
                });
        }
    }
}
