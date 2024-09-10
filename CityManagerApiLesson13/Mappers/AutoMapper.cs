using AutoMapper;
using CityManagerApiLesson13.DTO;
using CityManagerApiLesson13.Entities;

namespace CityManagerApiLesson13.Mappers;
public class AutoMapper : Profile
{
    // default constructor : 
    public AutoMapper()
    {
        // adding mappers : 
        CreateMap<City, CityForListDTO>()
            .ForMember(dest => dest.PhotoUrl, option =>
            {
                option.MapFrom(src => src.CityImages.FirstOrDefault(c => c.IsMain).Url);
            });

        CreateMap<City,CityDTO>().ReverseMap();
        CreateMap<User, UserForListDTO>();
        CreateMap<CityImage,CityImageDTO>();
    }
}
