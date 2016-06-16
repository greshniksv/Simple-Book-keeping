using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediatR;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Queries;

namespace SimpleBookKeeping.Controllers
{
    public class ExpenditureController : Controller
    {
        private readonly IMediator _mediator;
        private readonly Guid _userId;

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller" /> class.</summary>
        public ExpenditureController(IMediator mediator)
        {
            _userId = ((UserIndentity)HttpContext.User.Identity).Id;
            _mediator = mediator;
        }

        // GET: Expenditure
        public ActionResult Index(Guid costId)
        {
            var expenditures =
                _mediator.Send(new ExpendituresQuery() { CostId = costId, UserId = _userId });
            return View();
        }


    }
}