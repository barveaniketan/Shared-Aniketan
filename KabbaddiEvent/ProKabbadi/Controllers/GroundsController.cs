using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KabbaddiEvent;
using ProKabbadi.Models;

namespace ProKabbadi.Controllers
{
    public class GroundsController : Controller
    {
        private ProKabbadiContext db = new ProKabbadiContext();

        // GET: Grounds
        public ActionResult Index()
        {
            return View("Index", db.Grounds.ToList());
        }

        // GET: Grounds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ground ground = db.Grounds.Find(id);
            if (ground == null)
            {
                return HttpNotFound();
            }
            return View(ground);
        }

        // GET: Grounds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grounds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroundId,GroundName,Address,City")] Ground ground)
        {
            if (ModelState.IsValid)
            {
                db.Grounds.Add(ground);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ground);
        }

        // GET: Grounds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ground ground = db.Grounds.Find(id);
            if (ground == null)
            {
                return HttpNotFound();
            }
            return View(ground);
        }

        // POST: Grounds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroundId,GroundName,Address,City")] Ground ground)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ground).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ground);
        }

        // GET: Grounds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ground ground = db.Grounds.Find(id);
            if (ground == null)
            {
                return HttpNotFound();
            }
            return View(ground);
        }

        // POST: Grounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ground ground = db.Grounds.Find(id);
            db.Grounds.Remove(ground);
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
