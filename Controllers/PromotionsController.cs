using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeautyWebApp.Models;
using BeautyWebApp.ViewModels;

namespace BeautyWebApp.Controllers
{
    [Authorize]
    public class PromotionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Dropdown look ups

        private List<SelectListItem> costs = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "", Value= "" },
                new SelectListItem(){ Text = "Free", Value= "Free" },
                new SelectListItem(){ Text = "R5OO", Value= "R5OO" },
                new SelectListItem(){ Text = "R1000", Value= "R1000" },
                new SelectListItem(){ Text = "R2000", Value= "R2000" },
            };

        private List<SelectListItem> promotionTypes = new List<SelectListItem>()
        {
            new SelectListItem(){ Text = "", Value= "" },
            new SelectListItem(){ Text = "Hair cut and treatment", Value= "Hair cut and treatment" },
            new SelectListItem(){ Text = "Hair and beard trim", Value= "Hair and beard trim" },
            new SelectListItem(){ Text = "Full set", Value= "Full set" },
        };

        #endregion


        // GET: Promotions
        public ActionResult Index()
        {
            return View(db.Promotions.ToList());
        }

        // GET: Promotions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            PromotionVM detail = new PromotionVM() 
            { 
                Cost = promotion.Cost,
                PromotionDate = promotion.PromotionDate,
                Description = promotion.Description,
                Duration = promotion.Duration,
                Id = promotion.Id,
                PromotionType = promotion.PromotionType,
                Venue = promotion.Venue,
                PromotionCreatorId = promotion.PromotionCreatorId
            };
            //attendees:
            var promotionAttendees = db.PromotionAttendees.Where(x => x.PromotionId == promotion.Id).ToList();
            if(promotionAttendees.Count > 0)
            {
                int[] ids = promotionAttendees.Select(x => x.AttendeeId).ToArray();
                detail.Attendees = db.Attendees.Where(x => ids.Contains(x.Id)).ToList();
            }
            return View(detail);
        }

        // GET: Promotions/Create
        public ActionResult Create()
        {
            ViewBag.Costs = costs;
            ViewBag.PromotionTypes = promotionTypes;
            return View();
        }

        // POST: Promotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            PromotionVM promotion)
        {
            if (ModelState.IsValid)
            {
                //promotion creator:
                PromotionCreator creator = new PromotionCreator()
                {
                    Email = promotion.Email,
                    FirstName = promotion.FirstName,
                    LastName = promotion.LastName,
                    PhoneNumber = promotion.PhoneNumber
                };
                db.PromotionCreators.Add(creator);
                db.SaveChanges();

                Promotion model = new Promotion()
                {
                    Cost = promotion.Cost,
                    Description = promotion.Description,
                    Duration = promotion.Duration.Value,
                    PromotionDate = promotion.PromotionDate.Value,
                    PromotionType = promotion.PromotionType,
                    Venue = promotion.Venue,
                    PromotionCreatorId = creator.Id
                };
                db.Promotions.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Costs = costs;
            ViewBag.PromotionTypes = promotionTypes;
            return View(promotion);
        }

        // GET: Promotions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            PromotionCreator creator = db.PromotionCreators.Find(promotion.PromotionCreatorId);
            PromotionVM prom = new PromotionVM() 
            { 
                Cost = promotion.Cost,
                Description = promotion.Description,
                Duration = promotion.Duration,
                Id = promotion.Id,
                PromotionCreatorId = promotion.PromotionCreatorId,
                PromotionDate = promotion.PromotionDate,
                PromotionType = promotion.PromotionType,
                Venue = promotion.Venue,
                Email = creator.Email,
                FirstName = creator.FirstName,
                LastName = creator.LastName,
                PhoneNumber = creator.LastName
            };
            ViewBag.Costs = costs;
            ViewBag.PromotionTypes = promotionTypes;
            return View(prom);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            PromotionVM promotion)
        {
            if (ModelState.IsValid)
            {
                Promotion model = db.Promotions.Find(promotion.Id);
                PromotionCreator creator = db.PromotionCreators.Find(promotion.PromotionCreatorId);
                //promotion:
                model.Cost = promotion.Cost;
                model.Description = promotion.Description;
                model.Duration = promotion.Duration.Value;
                model.PromotionDate = promotion.PromotionDate.Value;
                model.PromotionType = promotion.PromotionType;
                model.Venue = promotion.Venue;

                //creator:
                creator.FirstName = promotion.FirstName;
                creator.Email = promotion.Email;
                creator.LastName = promotion.LastName;
                creator.PhoneNumber = promotion.PhoneNumber;

                db.Entry(model).State = EntityState.Modified;
                db.Entry(creator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Costs = costs;
            ViewBag.PromotionTypes = promotionTypes;
            return View(promotion);
        }


        [AllowAnonymous]
        public JsonResult Summaries()
        {
            List<PromotionSummaryVM> summaries = new List<PromotionSummaryVM>();
            var promotions = db.Promotions.Where(x => x.PromotionDate > DateTime.Now).ToList();
            foreach(var promotion in promotions)
            {
                PromotionSummaryVM summary = new PromotionSummaryVM();
                summary.title = promotion.Description;
                summary.id = promotion.Id;
                summary.venue = promotion.Venue;
                summary.date = string.Format("{0}-{1}-{2}", promotion.PromotionDate.Year, promotion.PromotionDate.Month, promotion.PromotionDate.Day);
                //promotion creator:
                var creator = db.PromotionCreators.Find(promotion.PromotionCreatorId);
                summary.FirstName = creator.FirstName;
                summary.LastName = creator.LastName;
                summary.PhoneNumber = creator.PhoneNumber;
                summary.Email = creator.Email;
                summary.url = string.Format("/attendees/Create/{0}", promotion.Id);
                summaries.Add(summary);
            }
            return Json(summaries,JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
