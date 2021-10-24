using Application.Interfaces.Commands;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Packages
{
    public class DeactivatePackageCommand : IDeactivateCommand
    {
        public int Id => 6;

        public string Name => "DeacktivacijaBankovnog paketa";

        private readonly Context _context;

        public DeactivatePackageCommand(Context context)
        {
            _context = context;
        }

        public void Execute(int request)
        {
            var package = _context.Packages.Where(x => x.DeleteAt == null && x.Active == true).FirstOrDefault(x => x.Id == request);

            if (package == null)
            {
                throw new Exception("Bankovni paket nepostoji");
            }

            package.Active = false;

            _context.SaveChanges();
        }
    }
}
