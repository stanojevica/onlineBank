using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core
{
    public class UnregistredUser : IApplicationUser
    {
        public int Id => 0;

        public string Identity => "Neregistrovani korisnik";

        public IEnumerable<int> AllowedUseCases => new List<int> {14,15, 17, 33};

        public int RoleId => 0;
    }
}
