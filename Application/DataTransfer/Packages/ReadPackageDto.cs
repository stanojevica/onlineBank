using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Packages
{
    public class ReadPackageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal AccountMaintenance { get; set; }
        public int? NumOfUsers { get; set; }
    }
}
