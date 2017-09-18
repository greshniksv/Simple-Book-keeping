using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediatR;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Extensions;
using SimpleBookKeeping.Models;
using SimpleBookKeeping.Queries;

namespace SimpleBookKeeping.Helpers
{
    public class MenuHelper
    {
        public static IReadOnlyCollection<PlanCostsModel> GetPlanCosts()
        {
            var userId = HttpContext.Current.UserId();
            IReadOnlyCollection<PlanCostsModel> planCosts;

            var mediator = MvcApp.Unity.Resolve<IMediator>();
            //try
            //{
                planCosts = mediator.Send(new GetActivePlanCostsQuery { UserId = userId });
            //}
            //catch (Exception)
            //{
            //    planCosts = new List<PlanCostsModel>();
            //}
            return planCosts.OrderBy(x=>x.Name).ToList();
        }
    }
}