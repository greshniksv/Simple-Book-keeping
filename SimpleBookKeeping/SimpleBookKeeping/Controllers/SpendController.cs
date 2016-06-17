using System;
using System.Web.Mvc;
using MediatR;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Queries;


namespace SimpleBookKeeping.Controllers
{
    public class SpendController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller" /> class.</summary>
        public SpendController()
        {
            
            _mediator = MvcApp.Unity.Resolve<IMediator>();
        }

        // GET: Spend
        public ActionResult Index(Guid costId)
        {
            var userId = ((UserIndentity)HttpContext.User.Identity).Id;

            var costSpends = _mediator.Send(new GetActiveCostSpendDetailsQuery());

            //var spends =
            //    _mediator.Send(new GetSpendsQuery() { CostId = costId, UserId = _userId });

            return View();
        }


    }
}