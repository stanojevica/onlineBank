using Application.DataTransfer.Users.Transactions;
using AutoMapper;
using Domain.Etities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDto>()
                .ForMember(x => x.RecipientAccountNumber, y => y.MapFrom(r => r.Recipient.Account.AccountNumber))
                .ForMember(x => x.Amount, y => y.MapFrom(t => t.Amount))
                .ForMember(x => x.Purpose, y => y.MapFrom(t => t.Purpose));
        }
    }
}
