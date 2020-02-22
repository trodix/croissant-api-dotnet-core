using AutoMapper;
using CroissantApi.Models;
using CroissantApi.Resources;

namespace CroissantApi.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
        }
    }
}