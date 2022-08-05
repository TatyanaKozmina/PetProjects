using AutoMapper;
using SchoolJournal.Models;
using SchoolJournal.Models.DB;

namespace SchoolJournal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterModel, User>().ReverseMap();
        }
    }
}
