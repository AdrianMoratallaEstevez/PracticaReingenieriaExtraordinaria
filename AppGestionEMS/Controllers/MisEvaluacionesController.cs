using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppGestionEMS.Models;
using Microsoft.AspNet.Identity;

namespace AppGestionEMS.Controllers
{
    [Authorize(Roles = "alumno")]

    public class MisEvaluacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MisEvaluaciones
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            var evaluaciones = db.Evaluaciones.Include(e => e.Curso).Include(e => e.Alumno).Where(p
            => p.AlumnoId == currentUserId);
            return View(evaluaciones.ToList());
        }


        // GET: MisEvaluaciones/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluaciones evaluaciones = db.Evaluaciones.Find(id);
            if (evaluaciones == null)
            {
                return HttpNotFound();
            }
            return View(evaluaciones);
        }

        // GET: MisEvaluaciones/Create
        public ActionResult Create()
        {
            ViewBag.AlumnoId = new SelectList(db.Users, "Id", "Nombre");
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nombre");
            return View();
        }

        // POST: MisEvaluaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlumnoId,CursoId,nota")] Evaluaciones evaluaciones)
        {
            if (ModelState.IsValid)
            {
                db.Evaluaciones.Add(evaluaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlumnoId = new SelectList(db.Users, "Id", "Nombre", evaluaciones.AlumnoId);
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nombre", evaluaciones.CursoId);
            return View(evaluaciones);
        }

        // GET: MisEvaluaciones/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluaciones evaluaciones = db.Evaluaciones.Find(id);
            if (evaluaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlumnoId = new SelectList(db.Users, "Id", "Nombre", evaluaciones.AlumnoId);
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nombre", evaluaciones.CursoId);
            return View(evaluaciones);
        }

        // POST: MisEvaluaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlumnoId,CursoId,nota")] Evaluaciones evaluaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evaluaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlumnoId = new SelectList(db.Users, "Id", "Nombre", evaluaciones.AlumnoId);
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nombre", evaluaciones.CursoId);
            return View(evaluaciones);
        }

        // GET: MisEvaluaciones/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluaciones evaluaciones = db.Evaluaciones.Find(id);
            if (evaluaciones == null)
            {
                return HttpNotFound();
            }
            return View(evaluaciones);
        }

        // POST: MisEvaluaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Evaluaciones evaluaciones = db.Evaluaciones.Find(id);
            db.Evaluaciones.Remove(evaluaciones);
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
