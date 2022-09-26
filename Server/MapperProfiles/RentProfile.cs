using AutoMapper;
using Entities.DataTransferObjects.RentDTO;
using Entities.Models;

namespace Server.MapperProfiles
{
    public class RentProfile : Profile
    {
        public RentProfile()
        {
            CreateMap<Rent, RentToShowDto>()
                               .ForMember(c => c.RentalCarName, opt => opt.MapFrom(x => x.Car.Name));
            CreateMap<RentToCreateDto, Rent>();
        }

    }
}
