using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediatR;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Models;
using SimpleBookKeeping.Queries;

namespace SimpleBookKeeping.Helpers
{
    public class MenuHelper
    {
        public static IReadOnlyCollection<PlanCostsModel> GetPlanCosts()
        {
            var userId = ((UserIndentity)HttpContext.Current.User.Identity).Id;

            var mediator = MvcApp.Unity.Resolve<IMediator>();
            IReadOnlyCollection<PlanCostsModel> planCosts = 
                mediator.Send(new GetActivePlanCostsQuery { UserId = userId });
            return planCosts;
        }
    }
}