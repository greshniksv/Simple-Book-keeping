using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBookKeeping.Controllers
{
    public class PlanController : Controller
    {
        // GET: Paln
        public ActionResult Index()
        {
            return View();
        }

        // GET: CreatePlan
        public ActionResult CreatePlan()
        {
            return View("CreatePlan");
        }


    }
}