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
    public class DeleteUserCommand : IDeleteCommand
    {
        public int Id => 11;

        public string Name => "Brisanje korisnika";

        private readonly Context _context;

        public DeleteUserCommand(Context context)
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

            if (user.Account.DeleteAt != null || user.Account.Active == false)
            {
                throw new Exception("Korisnk ima otvoren racun i ne mozete ga obrisati");
            }

            if (user.CreditUsers.Any(x => x.CreditStatuses.Any(x => x.Status == Domain.Etities.Status.Odobren) || x.CreditStatuses.Any(x => x.Status == Domain.Etities.Status.NaCekanju) || x.CreditStatuses.Any(x => x.Status == Domain.Etities.Status.Aktivan)))
            {
                throw new Exception("Korisnk ima kredit i ne mozete ga obrisati");
            }
            user.DeleteAt = DateTime.Now;
            user.Active = false;

            _context.SaveChanges();
        }
    }
}
