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

                cfg.CreateMap<Cost, CostModel>();
                cfg.CreateMap<CostModel, Cost>();

                cfg.CreateMap<CostDetail, CostDetailModel>();
                cfg.CreateMap<CostDetailModel, CostDetail>();

            });

            Mapper = Configuration.CreateMapper();
        }
    }
}