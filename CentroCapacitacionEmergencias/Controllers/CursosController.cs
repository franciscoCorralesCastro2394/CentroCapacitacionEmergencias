using CentroCapacitacionEmergencias.Data;
using CentroCapacitacionEmergencias.Models;
using CentroCapacitacionEmergencias.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentroCapacitacionEmergencias.Controllers
{
    public class CursosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cursos
        //public ActionResult Create()
        //{
        //    return View();
        //}

        public ActionResult Create()
        {
            var model = new CursoViewModel();

            int idRol = db.Roles.FirstOrDefault(r => r.Nombre == "Instructor").Id;

            model.Instructores = db.Usuarios
                .Where(u => u.RolId == idRol)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Nombre
                }).ToList();

            model.Cohortes = db.Cohortes
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CursoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Curso curso = new Curso
                {
                    Titulo = model.Titulo,
                    CodigoCurso = model.CodigoCurso,
                    HorasPracticas = model.HorasPracticas
                };

                curso.Instructores = db.Usuarios
                    .Where(u => model.InstructoresSeleccionados.Contains(u.Id))
                    .ToList();

                curso.Cohortes = db.Cohortes
                    .Where(c => model.CohortesSeleccionadas.Contains(c.Id))
                    .ToList();

                db.Cursos.Add(curso);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}