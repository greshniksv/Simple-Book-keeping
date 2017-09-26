using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MediatR;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Commands;
using SimpleBookKeeping.Extensions;
using SimpleBookKeeping.Models;
using SimpleBookKeeping.Queries;
using SimpleBookKeeping.Unility;


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
            var userId = HttpContext.UserId();
            IList<CostSpendDetailModel> costSpend =
                 _mediator.Send(new GetActiveCostSpendDetailsQuery { UserId = userId, CostId = costId });

            var creditSpands = _mediator.Send(new GetCreditSpends() {UserId = userId, CostId = costId});

            ViewBag.Credits = creditSpands;
            ViewBag.UserId = userId;

            return View(costSpend);
        }

        public ActionResult Add(AddSpendModel model)
        {
            var userId = HttpContext.UserId();

            if (ModelState.IsValid)
            {
                _mediator.Send(new SaveSpendCommand {SpendModels = new[] {model}, UserId = userId });
            }

            return RedirectToAction("Index", new { costId = model.CostId });
        }

        public ActionResult AddCredit(AddCreditSpendModel model)
        {
            var userId = HttpContext.UserId();

            if (ModelState.IsValid)
            {
                _mediator.Send(new SaveCreditSpendCommand { SpendModels = new[] { model }, UserId = userId });
            }

            return RedirectToAction("Index", new { costId = model.CostId });
        }

        public ActionResult Update(AddSpendModel[] addSpendModels)
        {
            var userId = HttpContext.UserId();

            _mediator.Send(new SaveSpendCommand { SpendModels = addSpendModels, UserId = userId });

            return RedirectToAction("Index", new { costId = addSpendModels.First().CostId });
        }

        public ActionResult Image(Guid spendId)
        {
            var userId = HttpContext.UserId();

            var spend = _mediator.Send(new GetSpendQuery {SpendId = spendId});
            var costId = _mediator.Send(new GetCostBySpendQuery {SpendId = spendId});

            ViewBag.CostId = costId;

            var url = Url.Content("~/HttpHandler/DownloadFileHandler.ashx") + $"?f={spend.Image}";

            FileStorage storage = new FileStorage();
            var image = storage.Get(spend.Image);

            System.Drawing.Image img = System.Drawing.Image.FromFile(image.FullName);
            
            var json =
                $"[ {{ src: '{url}', w: {img.Width}, h: {img.Height} }} ];";

            ViewBag.Url = url;
            ViewBag.Width = img.Width;
            ViewBag.Height = img.Height;


            return View();
        }
    }
}