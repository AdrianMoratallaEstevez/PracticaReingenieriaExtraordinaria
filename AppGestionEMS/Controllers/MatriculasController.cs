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
    public class MatriculasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Matriculas
        public ActionResult Index()
        {
            var matriculas = db.Matriculas.Include(m => m.Curso).Include(m => m.Grupo).Include(m => m.Alumno);
            return View(matriculas.ToList());
        }

        // GET: Matriculas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriculas matriculas = db.Matriculas.Find(id);
            if (matriculas == null)
            {
                return HttpNotFound();
            }
            return View(matriculas);
        }

        // GET: Matriculas/Create
        public ActionResult Create()
        {
            var alumnos = from user in db.Users
                         from u_r in user.Roles
                         join rol in db.Roles on u_r.RoleId equals rol.Id
                         where rol.Name == "alumno"
                         select user.UserName;
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nombre");
            ViewBag.GrupoId = new SelectList(db.Grupoes, "Id", "NombreGrupo");
            ViewBag.UserId = new SelectList(db.Users.Where(u => alumnos.Contains(u.UserName)), "Id", "Nombre");
            return View();
        }

        // POST: Matriculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,CursoId,GrupoId")] Matriculas matriculas)
        {
            if (ModelState.IsValid)
            {
                db.Matriculas.Add(matriculas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nombre", matriculas.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupoes, "Id", "NombreGrupo", matriculas.GrupoId);
            ViewBag.Id = new SelectList(db.Users, "Id", "Nombre", matriculas.AlumnoId);
            return View(matriculas);
        }

        // GET: Matriculas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriculas matriculas = db.Matriculas.Find(id);
            if (matriculas == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nombre", matriculas.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupoes, "Id", "NombreGrupo", matriculas.GrupoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", matriculas.AlumnoId);
            return View(matriculas);
        }

        // POST: Matriculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,CursoId,GrupoId")] Matriculas matriculas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matriculas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nombre", matriculas.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupoes, "Id", "NombreGrupo", matriculas.GrupoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", matriculas.AlumnoId);
            return View(matriculas);
        }

        // GET: Matriculas/Delete/5
        public ActionResult Delete(int? curso, int? grupo, string user)
        {
            if (curso == null || grupo == null || user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriculas matricula = db.Matriculas.Find(user, curso, grupo);
            if (matricula == null)
            {
                return HttpNotFound();
            }
            return View(matricula);
        }

        // POST: Matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int curso, int grupo, string user)
        {
            Matriculas matricula = db.Matriculas.Find(user, curso, grupo);
            db.Matriculas.Remove(matricula);
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
