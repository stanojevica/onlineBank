using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Users.Credits
{
    public class CreditStatusDto
    {
        public int UserId { get; set; }
        public StatusDto Status { get; set; }
    }

    public enum StatusDto
    {
        NaCekanju,
        Odobren,
        NijeOdobren,
        Pauziran,
        Aktivan,
        Isplacen
    }
}
