using AutoMapper;
using WebApi.Entities;
using WebApi.Models.Users;
using WebApi.Models.Beers;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
            CreateMap<Beer, BeerModel>();
            CreateMap<AddBeerModel, Beer>();
            CreateMap<UpdateModel, Beer>();
        }
    }
}