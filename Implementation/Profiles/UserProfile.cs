using Application.DataTransfer.Users;
using AutoMapper;
using Domain.Etities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ReadUserDto>()
                .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
                .ForMember(x => x.FirstName, y => y.MapFrom(u => u.Name))
                .ForMember(x => x.LastName, y => y.MapFrom(u => u.LastName))
                .ForMember(x => x.Email, y => y.MapFrom(u => u.Email))
                .ForMember(x => x.AccountNumber, y => y.MapFrom(u => u.Account.AccountNumber))
                .ForMember(x => x.AvaibleFounds, y => y.MapFrom(u => u.Account.AvailableFunds))
                .ForMember(x => x.IdentificationNumber, y => y.MapFrom(u => u.IdentityNumber))
                .ForMember(x => x.RoleId, y => y.MapFrom(u => u.RoleId))
                .ForMember(x => x.PackageType, y => y.MapFrom(u => u.Account.Package.Name))
                .ForMember(x => x.PackageId, y => y.MapFrom(u => u.Account.PackageId))
                .ForMember(x => x.CreateAt, y => y.MapFrom(u => u.CreatedAt))
                .ForMember(x => x.MonthsRecidev, y => y.MapFrom(t => t.TransactionSenders.OrderByDescending(t => t.Date).Select(s => s.Date.Month.ToString() + "-" + s.Date.Year.ToString()
                )))
                .ForMember(x => x.MonthsSent, y => y.MapFrom(t => t.TransactionRecipients.OrderByDescending(t => t.Date).Select(s => s.Date.Month.ToString() + "-" + s.Date.Year.ToString()
                )))
            /*.ForMember(x => x.Transactions, y => y.MapFrom(u => u.TransactionRecipients.Select(t =>
            new TransactionPerMonths
            {
                Date = t.Date.Month.ToString() + t.Date.Year.ToString()
            })))*/
            .ForMember(x => x.ReciivedTransactions, y => y.MapFrom(u => u.TransactionRecipients.OrderByDescending(u => u.Date).Select(t =>
            new ReadTransactionsDto
            {
                Name = t.Sender.Name+" "+t.Sender.LastName,
                Amount = t.Amount,
                Date = t.Date,
                Purpose = t.Purpose,
                RoleId = t.Sender.RoleId,
                Id = t.RecipientId
            })))
            .ForMember(x => x.SentTransactions, y => y.MapFrom(u => u.TransactionSenders.OrderByDescending(u => u.Date).Select(t =>
            new ReadTransactionsDto
            {
                Name = t.Recipient.Name+" "+t.Recipient.LastName,
                Amount = t.Amount,
                Date = t.Date,
                Purpose = t.Purpose,
                RoleId = t.Recipient.RoleId,
                Id = t.SenderId

            })))
            .ForMember(x => x.Credit, y => y.MapFrom(u => u.CreditUsers.Select(c =>
             new ReadUserCredit
             {
                 CreditName = c.Credit.Name,
                 CreditTypeName = c.Credit.CreditType.Name,
                 CreditStatus = c.CreditStatuses.OrderByDescending(x => x.CreatedAt).First().Status.ToString(),
                 Amount = c.Amount,
                 Years = c.Years,
                 MonthlyPayment = c.MonthlyPayment,
                 RemainingInstalments = c.RemainingInstalments,
                 Interest = c.Credit.CreditCalculations.FirstOrDefault(x => x.MaxAmount >= c.Amount && x.MinAmount <= c.Amount && x.MinYear <= c.Years && x.MaxYear >= c.Years).Interest
             })));
        }
    }
}
