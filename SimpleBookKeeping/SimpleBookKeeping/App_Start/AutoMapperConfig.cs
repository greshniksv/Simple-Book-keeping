using System.Linq;
using AutoMapper;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping
{
    public static class AutoMapperConfig
    {

        private static MapperConfiguration Configuration { get; set; }

        public static IMapper Mapper { get; set; }

        public static void RegisterMaps()
        {
            Configuration = new MapperConfiguration(cfg => {

                cfg.CreateMap<SpendModel, Spend>()
                    .ForMember(dst => dst.User, opt => opt.Ignore());

                cfg.CreateMap<Spend, SpendModel>()
                    .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.User.Id));

                cfg.CreateMap<Plan, PlanCostsModel>()
                    .ForMember(dst => dst.UserMembers, opt => opt.MapFrom(src => src.PlanMembers.Select(x => x.User.Id)));

                cfg.CreateMap<Plan, PlanModel>()
                    .ForMember(dst => dst.UserMembers, opt => opt.MapFrom(src => src.PlanMembers.Select(x=>x.User.Id)));

                cfg.CreateMap<PlanModel, Plan>()
                    .ForMember(dst => dst.PlanMembers, opt => opt.Ignore());

                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserModel, User>();
                
                //cfg.CreateMap<PlanMember, PlanMemberModel>();
                //cfg.CreateMap<PlanMemberModel, PlanMember>();

                cfg.CreateMap<Cost, CostModel>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.PlanId, opt => opt.MapFrom(src => src.Plan.Id))
                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));

                cfg.CreateMap<CostModel, Cost>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dst => dst.CostDetails, opt => opt.Ignore());

                cfg.CreateMap<CostDetail, CostDetailModel>();
                cfg.CreateMap<CostDetailModel, CostDetail>();

            });

            Mapper = Configuration.CreateMapper();
        }
    }
}