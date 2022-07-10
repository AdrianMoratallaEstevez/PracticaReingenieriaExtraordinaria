using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppGestionEMS.Models;

namespace AppGestionEMS.Controllers
{
    [Authorize(Roles = "profesor, jefedpto")]

    public class EvaluacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Evaluaciones
        public ActionResult Index()
        {
            var evaluaciones = db.Evaluaciones.Include(e => e.Alumno).Include(e => e.Curso);
            return View(evaluaciones.ToList());
        }

        // GET: Evaluaciones/Details/5
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

        // GET: Evaluaciones/Create
        public ActionResult Create()
        {
            var alumnos = from user in db.Users
                         from u_r in user.Roles
                         join rol in db.Roles on u_r.RoleId equals rol.Id
                         where rol.Name == "alumno"
                         select user.UserName;
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nombre");
            ViewBag.AlumnoId = new SelectList(db.Users.Where(u => alumnos.Contains(u.UserName)), "Id", "Nombre");
            return View();
        }

        // POST: Evaluaciones/Create
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

        // GET: Evaluaciones/Edit/5
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

        // POST: Evaluaciones/Edit/5
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

        // GET: Evaluaciones/Delete/5
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

        // POST: Evaluaciones/Delete/5
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
