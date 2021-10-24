using Application.DataTransfer.Packages;
using Application.Interfaces.Commands;
using DataAccess;
using Domain.Etities;
using FluentValidation;
using Implementation.Validators.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Packages
{
    public class CreatePackageCommand : ICreatePackageCommand
    {
        public int Id => 1;

        public string Name => "Kreiranje novog bankovnog pačuna";

        private readonly Context _context;
        private readonly CreatePackageValidator _validator;

        public CreatePackageCommand(Context context, CreatePackageValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(PackageDto request)
        {
            _validator.ValidateAndThrow(request);
            var newPackage = new Package
            {
                Name = request.Name,
                Description = request.Description,
                AccountMaintenance = request.AccountMaintenance,
                UpdateAt = null,
                DeleteAt = null,
                Active = true,
                CreatedAt = DateTime.Now
            };
            _context.Packages.Add(newPackage);
            _context.SaveChanges();
        }
    }
}
