using AutoMapper;
using testWork.Data.Domain;
using testWork.Dtos.Incident;

namespace testWork.Profiles
{
    public class IncidentProfile : Profile
    {
        public IncidentProfile()
        {
            CreateMap<Incident, ReadIncidentDto>();
        }
    }
}