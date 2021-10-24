using Application.Interfaces.Commands;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Packages
{
    public class DeletePackageCommand : IDeleteCommand
    {
        public int Id => 7;

        public string Name => "Brisanje bankovnog paketa";
        
        private readonly Context _context;

        public DeletePackageCommand(Context context)
        {
            _context = context;
        }

        public void Execute(int request)
        {
            var package = _context.Packages.Where(x => x.DeleteAt == null)
                .FirstOrDefault(x => x.Id == request);

            if (package == null)
            {
                throw new Exception("Bankovni paket ne postoji");
            }

            if (package.Accounts.Count() > 0)
            {
                throw new Exception("Bankovni paket koriste klijenti i nije moguće obrisati ga.");
            }
            package.DeleteAt = DateTime.Now;
            package.Active = false;

            _context.SaveChanges();
        }
    }
}
