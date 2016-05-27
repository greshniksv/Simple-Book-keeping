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
                cfg.CreateMap<CostPlan, PlanModel>();
                cfg.CreateMap<PlanModel, CostPlan>();
            });

            Mapper = Configuration.CreateMapper();
        }
    }
}