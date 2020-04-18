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
            CreateMap<User, UserLightResource>();
            CreateMap<UserRule, UserRuleResource>();
            CreateMap<UserRule, UserRuleWithoutUserResource>();
            CreateMap<Rule, RuleResource>();
            CreateMap<Team, TeamResource>();
            CreateMap<Team, TeamLightResource>();
            CreateMap<Team, TeamWithUsersResource>();
            CreateMap<TeamRule, TeamRuleResource>();
        }
    }
}