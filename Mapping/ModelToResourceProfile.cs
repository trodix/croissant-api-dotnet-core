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
            CreateMap<UserRule, UserRuleResource>();
            CreateMap<Rule, RuleResource>();
            CreateMap<Team, TeamResource>();
        }
    }
}