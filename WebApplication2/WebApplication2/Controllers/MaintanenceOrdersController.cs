using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MaintanenceOrdersController : Controller
    {
        private leasingEntities db = new leasingEntities();

        // GET: MaintanenceOrders
        public ActionResult Index()
        {
            return View(db.MaintanenceOrders.ToList());
        }

        // GET: MaintanenceOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintanenceOrder maintanenceOrder = db.MaintanenceOrders.Find(id);
            if (maintanenceOrder == null)
            {
                return HttpNotFound();
            }
            return View(maintanenceOrder);
        }

        // GET: MaintanenceOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaintanenceOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,ApartmentID,UserID,ProblemLocation,ProblemDescription,ManagerApproval,MaintanenceVisit,MaintanenceCompletion,UserFeedbackStar,UserFeedback")] MaintanenceOrder maintanenceOrder)
        {
            if (ModelState.IsValid)
            {
                db.MaintanenceOrders.Add(maintanenceOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(maintanenceOrder);
        }

        // GET: MaintanenceOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintanenceOrder maintanenceOrder = db.MaintanenceOrders.Find(id);
            if (maintanenceOrder == null)
            {
                return HttpNotFound();
            }
            return View(maintanenceOrder);
        }

        // POST: MaintanenceOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,ApartmentID,UserID,ProblemLocation,ProblemDescription,ManagerApproval,MaintanenceVisit,MaintanenceCompletion,UserFeedbackStar,UserFeedback")] MaintanenceOrder maintanenceOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintanenceOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maintanenceOrder);
        }

        // GET: MaintanenceOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintanenceOrder maintanenceOrder = db.MaintanenceOrders.Find(id);
            if (maintanenceOrder == null)
            {
                return HttpNotFound();
            }
            return View(maintanenceOrder);
        }

        // POST: MaintanenceOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintanenceOrder maintanenceOrder = db.MaintanenceOrders.Find(id);
            db.MaintanenceOrders.Remove(maintanenceOrder);
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
