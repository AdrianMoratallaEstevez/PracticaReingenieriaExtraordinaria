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
    [Authorize(Roles = "jefedpto")]

    public class AsignacionDocentesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AsignacionDocentes
        public ActionResult Index()
        {
            var asignacionDocentes = db.AsignacionDocentes.Include(a => a.Curso).Include(a => a.Grupo).Include(a => a.Profesor);
            return View(asignacionDocentes.ToList());
        }

        // GET: AsignacionDocentes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsignacionDocente asignacionDocente = db.AsignacionDocentes.Find(id);
            if (asignacionDocente == null)
            {
                return HttpNotFound();
            }
            return View(asignacionDocente);
        }

        // GET: AsignacionDocentes/Create
        public ActionResult Create()
        {
            var profes = from user in db.Users
                         from u_r in user.Roles
                         join rol in db.Roles on u_r.RoleId equals rol.Id
                         where rol.Name == "profesor"
                         select user.UserName;
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nombre");
            ViewBag.GrupoId = new SelectList(db.Grupoes, "Id", "NombreGrupo");
            ViewBag.UserId = new SelectList(db.Users.Where(u => profes.Contains(u.UserName)), "Id", "Nombre");
            return View();
        }

        // POST: AsignacionDocentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProfesorId,CursoId,GrupoId")] AsignacionDocente asignacionDocente)
        {
            if (ModelState.IsValid)
            {
                db.AsignacionDocentes.Add(asignacionDocente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nombre", asignacionDocente.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupoes, "Id", "NombreGrupo", asignacionDocente.GrupoId);
            ViewBag.ProfesorId = new SelectList(db.Users, "Id", "Nombre", asignacionDocente.ProfesorId);
            return View(asignacionDocente);
        }

        // GET: AsignacionDocentes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsignacionDocente asignacionDocente = db.AsignacionDocentes.Find(id);
            if (asignacionDocente == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nombre", asignacionDocente.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupoes, "Id", "NombreGrupo", asignacionDocente.GrupoId);
            ViewBag.ProfesorId = new SelectList(db.Users, "Id", "Nombre", asignacionDocente.ProfesorId);
            return View(asignacionDocente);
        }

        // POST: AsignacionDocentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProfesorId,CursoId,GrupoId")] AsignacionDocente asignacionDocente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asignacionDocente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nombre", asignacionDocente.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupoes, "Id", "NombreGrupo", asignacionDocente.GrupoId);
            ViewBag.ProfesorId = new SelectList(db.Users, "Id", "Nombre", asignacionDocente.ProfesorId);
            return View(asignacionDocente);
        }

        // GET: AsignacionDocentes/Delete/5
        public ActionResult Delete(int? curso, int? grupo, string user)
        {
            if (curso == null || grupo == null || user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsignacionDocente asignacionDocentes = db.AsignacionDocentes.Find(user, curso, grupo);
            if (asignacionDocentes == null)
            {
                return HttpNotFound();
            }
            return View(asignacionDocentes);
        }


        // POST: AsignacionDocentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int curso, int grupo, string user)
        {
            AsignacionDocente asignacionDocentes = db.AsignacionDocentes.Find(user, curso, grupo);
            db.AsignacionDocentes.Remove(asignacionDocentes);
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
