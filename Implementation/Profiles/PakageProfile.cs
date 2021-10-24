using Application.DataTransfer.Packages;
using AutoMapper;
using Domain.Etities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles
{
    public class PakageProfile : Profile
    {
        public PakageProfile()
        {
            CreateMap<Package, ReadPackageDto>()
                .ForMember(x => x.Id, y => y.MapFrom(p => p.Id))
                .ForMember(x => x.Name, y => y.MapFrom(p => p.Name))
                .ForMember(x => x.Description, y => y.MapFrom(p => p.Description))
                .ForMember(x => x.AccountMaintenance, y => y.MapFrom(p => p.AccountMaintenance))
                .ForMember(x => x.NumOfUsers, y => y.MapFrom(p => p.Accounts.Count()));
        }
    }
}
