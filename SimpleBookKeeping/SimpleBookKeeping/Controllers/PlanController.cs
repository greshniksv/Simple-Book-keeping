using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using SimpleBookKeeping.Attributes;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Exceptions;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Controllers
{
    //[HandleAllError]
    public class PlanController : Controller
    {
        // GET: Plan
        public ActionResult Index()
        {
            var plans = Db.Session.QueryOver<CostPlan>().List<CostPlan>();

            return View(plans);
        }

        // GET: Edit
        public ActionResult EditById(Guid id)
        {
            var plans = Db.Session.QueryOver<CostPlan>().Where(p=>p.Id == id).List();

            if (!plans.Any())
            {
                throw new PlanNotFoundException($"Plan id: {id}");
            }

            PlanModel model = AutoMapperConfig.Mapper.Map<PlanModel>(plans.First());

            TempData["model"] = model;
            return RedirectToAction("Edit");
        }

        public ActionResult Edit(PlanModel model)
        {
            var tempModel = TempData["model"] as PlanModel;
            if (tempModel != null)
            {
                model = tempModel;
                ModelState.Clear();
            }

            return View("Edit", model);
        }

        public ActionResult Create(PlanModel model)
        {
            if (ModelState.IsValid)
            {
                CostPlan plan = AutoMapperConfig.Mapper.Map<CostPlan>(model);
                Db.Session.Save(plan);
                return RedirectToAction("Index");
            }

            TempData["model"] = model;
            return RedirectToAction("Edit");
        }


    }
}