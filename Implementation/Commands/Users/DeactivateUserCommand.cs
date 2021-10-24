using Application.Interfaces.Commands;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Users
{
    public class DeactivateUserCommand : IDeactivateCommand
    {
        public int Id => 10;

        public string Name => "Deaktiviranje korisnika";

        private readonly Context _context;

        public DeactivateUserCommand(Context context)
        {
            _context = context;
        }

        public void Execute(int request)
        {
            var user = _context.Users
                .Include(x => x.Account)
                .Include(x => x.CreditUsers)
                .Where(x => x.DeleteAt == null)
                .FirstOrDefault(x => x.Id == request);

            if (user == null)
            {
                throw new Exception("Korisnik ne postoji");
            }

            if (user.Active == false)
            {
                throw new Exception("Korisnik je vec deaktiviran");
            }

            if (user.Account.DeleteAt != null || user.Account.Active == false)
            {
                throw new Exception("Korisnk ima otvoren racun i ne mozete ga deaktivirati");
            }

            if (user.CreditUsers.Any(x => x.CreditStatuses.Any(x => x.Status == Domain.Etities.Status.Odobren) || x.CreditStatuses.Any(x => x.Status == Domain.Etities.Status.NaCekanju) || x.CreditStatuses.Any(x => x.Status == Domain.Etities.Status.Aktivan)))
            {
                throw new Exception("Korisnk ima kredit i ne mozete ga deaktivirati");
            }

            user.Active = false;

            _context.SaveChanges();
        }
    }
}
