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

                cfg.CreateMap<Plan, PlanModel>();
                cfg.CreateMap<PlanModel, Plan>();

                cfg.CreateMap<Cost, CostModel>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.PlanId, opt => opt.MapFrom(src => src.Plan.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));
                //.ForMember(dst => dst.CostDetails, opt => opt.Ignore());

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