using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projet_Audit.Models;

namespace Projet_Audit.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RisquesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Risques
        public ActionResult Index()
        {
            return View(db.Risques.ToList());
        }

        // GET: Risques/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Risque risque = db.Risques.Find(id);
            if (risque == null)
            {
                return HttpNotFound();
            }
            return View(risque);
        }

        // GET: Risques/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Risques/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_risque,Type,score")] Risque risque)
        {
            if (ModelState.IsValid)
            {
                risque.score = 0;
                db.Risques.Add(risque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(risque);
        }

        // GET: Risques/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Risque risque = db.Risques.Find(id);
            if (risque == null)
            {
                return HttpNotFound();
            }
            return View(risque);
        }

        // POST: Risques/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_risque,Type,score")] Risque risque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(risque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(risque);
        }

        // GET: Risques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Risque risque = db.Risques.Find(id);
            if (risque == null)
            {
                return HttpNotFound();
            }
            return View(risque);
        }

        // POST: Risques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Risque risque = db.Risques.Find(id);
            db.Risques.Remove(risque);
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
