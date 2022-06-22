using AutoMapper;
using testWork.Data.Domain;
using testWork.Dtos.Contact;

namespace testWork.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<CreateContactDto, Contact>();
            CreateMap<Contact, ReadContactDto>();
        }
    }
}