using Application.DataTransfer.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Commands.PackageCommands
{
    public interface IUpdatePackageCommand : ICommand<PackageDto>
    {
    }
}
