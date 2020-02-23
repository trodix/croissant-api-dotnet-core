using AutoMapper;
using CroissantApi.Models;
using CroissantApi.Resources;

namespace CroissantApi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();
            CreateMap<SaveUserRuleResource, UserRule>();
            CreateMap<UpdateUserRuleResource, UserRule>();
            CreateMap<SaveRuleResource, Rule>();
            CreateMap<SaveTeamResource, Team>();
        }
    }
}