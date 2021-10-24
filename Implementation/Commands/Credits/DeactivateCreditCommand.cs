using Application.Exceptions;
using Application.Interfaces.Commands;
using DataAccess;
using Domain.Etities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Credits
{
    public class DeactivateCreditCommand : IDeactivateCommand
    {
        public int Id => 2;

        public string Name => "Deaktivacija kredita";

        private readonly Context _context;

        public DeactivateCreditCommand(Context context)
        {
            _context = context;
        }

        public void Execute(int request)
        {
            var credit = _context.Credits.FirstOrDefault(x => x.Id == request && x.DeleteAt == null);

            if (credit == null)
            {
                throw new EntityNotFoundException(typeof(Credit));
            }

            if (credit.Active == false)
            {
                throw new Exception("Kredit je vec dektiviran");
            }

            credit.Active = false;

            _context.SaveChanges();
        }
    }
}
