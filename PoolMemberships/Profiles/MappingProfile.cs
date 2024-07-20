using AutoMapper;
using PoolMemberships.Dtos;
using PoolMemberships.Models;

namespace PoolMemberships.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Membership, MembershipWithPersonDto>()
            .ForMember(dest => dest.PersonFirstName, opt => opt.MapFrom(src => src.Person.FirstName))
            .ForMember(dest => dest.PersonLastName, opt => opt.MapFrom(src => src.Person.LastName));
    }
}