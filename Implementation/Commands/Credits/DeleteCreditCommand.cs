using Application.Exceptions;
using Application.Interfaces.Commands;
using DataAccess;
using Domain.Etities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Credits
{
    public class DeleteCreditCommand : IDeleteCommand
    {
        public int Id => 3;

        public string Name => "Brisanje podataka o kreditu";

        private readonly Context _context;

        public DeleteCreditCommand(Context context)
        {
            _context = context;
        }

        public void Execute(int request)
        {
            var credit = _context.Credits
                .Include(x => x.CreditUsers)
                .Where(x => x.DeleteAt == null)
                .FirstOrDefault(x => x.Id == request);

            if (credit == null)
            {
                throw new EntityNotFoundException(typeof(Credit));
            }

            if (credit.CreditUsers.Any(x => x.CreditStatuses.Any(x => x.Status == Domain.Etities.Status.Odobren) || x.CreditStatuses.Any(x => x.Status == Domain.Etities.Status.NaCekanju) || x.CreditStatuses.Any(x => x.Status == Domain.Etities.Status.Aktivan)))
            {
                throw new Exception("Kredit koriste korisnici i ne moze se obrisati");
            }

            credit.DeleteAt = DateTime.Now;
            credit.Active = false;

            _context.SaveChanges();
        }
    }
}
