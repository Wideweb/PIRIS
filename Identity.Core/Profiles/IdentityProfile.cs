using AutoMapper;
using Identity.Core.Enumerations;
using Identity.Core.Features.Finance.Models;
using Identity.Core.Features.User.Models;
using Infrastructure.Models.Finance;
using Infrastructure.Models.Identity;

namespace Identity.Core.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<Client, ClientModel>();
            CreateMap<ClientModel, Client>();

            CreateMap<Deposit, DepositModel>();
            CreateMap<DepositModel, Deposit>();
            CreateMap<CreateDepositModel, Deposit>();

            CreateMap<Transaction, TransactionModel>();
            CreateMap<TransactionModel, Transaction>();

            CreateMap<Account, ClientAccountModel>();
            CreateMap<Account, AccountModel>();
            CreateMap<ClientAccountModel, Account>()
                .ForMember(dest => dest.AccountTypeId, opts => opts.UseValue((long)AccountTypeEnum.Client))
                .ForMember(dest => dest.Amount, opts => opts.Ignore());

            CreateMap<Account, BankAccountModel>();
            CreateMap<BankAccountModel, Account>()
                .ForMember(dest => dest.AccountTypeId, opts => opts.UseValue((long)AccountTypeEnum.Bank));

            CreateMap<CreateCreditModel, Credit>();
            CreateMap<CreditPlan, CreditPlanModel>();

            CreateMap<CreditCard, CreditCardModel>();
        }
    }
}
