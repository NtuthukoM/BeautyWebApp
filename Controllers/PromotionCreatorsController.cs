using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeautyWebApp.Models;

namespace BeautyWebApp.Controllers
{
    public class PromotionCreatorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PromotionCreators
        public ActionResult Index()
        {
            return View(db.PromotionCreators.ToList());
        }

        // GET: PromotionCreators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionCreator promotionCreator = db.PromotionCreators.Find(id);
            if (promotionCreator == null)
            {
                return HttpNotFound();
            }
            return View(promotionCreator);
        }

        // GET: PromotionCreators/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PromotionCreators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,PhoneNumber,Email")] PromotionCreator promotionCreator)
        {
            if (ModelState.IsValid)
            {
                db.PromotionCreators.Add(promotionCreator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(promotionCreator);
        }

        // GET: PromotionCreators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionCreator promotionCreator = db.PromotionCreators.Find(id);
            if (promotionCreator == null)
            {
                return HttpNotFound();
            }
            return View(promotionCreator);
        }

        // POST: PromotionCreators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,PhoneNumber,Email")] PromotionCreator promotionCreator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotionCreator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promotionCreator);
        }

        // GET: PromotionCreators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionCreator promotionCreator = db.PromotionCreators.Find(id);
            if (promotionCreator == null)
            {
                return HttpNotFound();
            }
            return View(promotionCreator);
        }

        // POST: PromotionCreators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PromotionCreator promotionCreator = db.PromotionCreators.Find(id);
            db.PromotionCreators.Remove(promotionCreator);
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
