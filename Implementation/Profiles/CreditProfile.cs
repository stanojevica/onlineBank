using Application.DataTransfer.Credits;
using AutoMapper;
using Domain.Etities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles
{
    public class CreditProfile : Profile
    {
        public CreditProfile() 
        {
            CreateMap<Credit, ReadCreditDto>()
                .ForMember(x => x.Id, y => y.MapFrom(c => c.Id))
                .ForMember(x => x.Name, y => y.MapFrom(c => c.Name))
                .ForMember(x => x.Description, y => y.MapFrom(c => c.Description))
                .ForMember(x => x.IdType, y => y.MapFrom(c => c.TypeId))
                .ForMember(x => x.NameOfType, y => y.MapFrom(c => c.CreditType.Name))
                .ForMember(x => x.Conditions, y => y.MapFrom(c => c.CreditConditions.Select(con => new CreditConditionDto
                {
                    Name = con.Name,
                    Condition = con.Condition
                })))
                .ForMember(x => x.Calculations, y=> y.MapFrom(c => c.CreditCalculations.Select(cal => new CreditCalculationDto 
                {
                    MaxAmount = cal.MaxAmount,
                    MaxYear = cal.MaxYear,
                    MinAmout = cal.MinAmount,
                    MinYear = cal.MinYear,
                    Interest = cal.Interest
                })));
        }

    }
}
