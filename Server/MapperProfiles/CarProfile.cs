using AutoMapper;
using Entities.DataTransferObjects.CarDTO;
using Entities.Models;

namespace Server.MapperProfiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarToShowDto>();
            CreateMap<CarToCreateDto, Car>();
            CreateMap<CarToUpdateDto, Car>();
        }
    }
}
