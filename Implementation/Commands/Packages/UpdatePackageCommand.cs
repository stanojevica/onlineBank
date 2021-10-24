using Application.DataTransfer.Packages;
using Application.Interfaces.Commands.PackageCommands;
using DataAccess;
using FluentValidation;
using Implementation.Validators.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Packages
{
    public class UpdatePackageCommand : IUpdatePackageCommand
    {
        public int Id => 8;

        public string Name => "Izmena podataka bankovnog paketa";

        public readonly Context _context;
        public readonly UpdatePackageValidator _validator;

        public UpdatePackageCommand(Context context, UpdatePackageValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(PackageDto request)
        {
            var package = _context.Packages
                .FirstOrDefault(x => x.Id == request.Id && x.DeleteAt == null);
            if (package == null)
            {
                throw new Exception("Bankovni paket ne postoji");
            }
            _validator.ValidateAndThrow(request);
            package.Name = request.Name;
            package.Description = request.Description;
            package.AccountMaintenance = request.AccountMaintenance;
            package.UpdateAt = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
