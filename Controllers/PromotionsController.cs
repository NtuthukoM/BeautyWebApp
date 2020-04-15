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
            return View(promotion);
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
        public ActionResult Create(/*[Bind(Include = "Id,PromotionCreatorId,Description,Duration,Venue,PromotionType,Cost,PromotionDate")]*/ 
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
            ViewBag.Costs = costs;
            ViewBag.PromotionTypes = promotionTypes;
            return View(promotion);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "Id,PromotionCreatorId,Description,Duration,Venue,PromotionType,Cost,PromotionDate")]*/ 
            PromotionVM promotion)
        {
            if (ModelState.IsValid)
            {
                Promotion model = db.Promotions.Find(promotion.Id);
                PromotionCreator creator = db.PromotionCreators.Find(promotion.PromotionCreatorId);


                db.Entry(model).State = EntityState.Modified;
                db.Entry(creator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Costs = costs;
            ViewBag.PromotionTypes = promotionTypes;
            return View(promotion);
        }

        // GET: Promotions/Delete/5
        public ActionResult Delete(int? id)
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
            return View(promotion);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promotion promotion = db.Promotions.Find(id);
            db.Promotions.Remove(promotion);
            db.SaveChanges();
            return RedirectToAction("Index");
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
