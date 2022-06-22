using AutoMapper;
using testWork.Data.Domain;
using testWork.Dtos.Account;

namespace testWork.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<CreateAccountDto, Account>()
                .ForMember(c => c.Contacts, opt => opt.Ignore());
               
            CreateMap<Account, ReadAccountDto>();
        }
    }
}