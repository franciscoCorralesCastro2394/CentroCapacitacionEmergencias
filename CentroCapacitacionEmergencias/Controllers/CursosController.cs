using CentroCapacitacionEmergencias.Data;
using CentroCapacitacionEmergencias.Models;
using CentroCapacitacionEmergencias.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

                // Asignar instructores y cohortes seleccionados
                curso.Instructores = db.Usuarios
                    .Where(u => model.InstructoresSeleccionados.Contains(u.Id))
                    .ToList();

                curso.Cohortes = db.Cohortes
                    .Where(c => model.CohortesSeleccionadas.Contains(c.Id))
                    .ToList();
                // Marcar el curso como archivado en True para que se muestre en la lista de cursos activos
                curso.Archivado = true;
                db.Cursos.Add(curso);
                db.SaveChanges();

                // Redirigir a la lista de cursos o a otra página relevante
                return RedirectToAction("Lista");

            }

            return View(model);
        }

        public ActionResult Lista()
        {
            // Obtener todas las cohortes de la base de datos
            List<Curso> cursos = db.Cursos.Where(c => c.Archivado == true).ToList();

            CursoViewModel cursoViewModel = new CursoViewModel();
            cursoViewModel.Cursos = cursos;

            return View(cursoViewModel);
        }

        public ActionResult Delete(int id)
        {
            Curso curso = db.Cursos.Find(id);
            if (curso != null)
            {
                // En lugar de eliminar el registro, Archivar Cursos
                // Si el curso ya está archivado, lo desarchiva, y viceversa
                curso.Archivado = !curso.Archivado;
                db.SaveChanges();
            }
            return RedirectToAction("Lista", "Cursos");
        }

        public ActionResult EditarCurso(CursoViewModel cursoViewModel)
        {
            if (cursoViewModel.Id != 0) { 

                Curso curso = new Curso
                {
                    Id = cursoViewModel.Id,
                    Titulo = cursoViewModel.Titulo,
                    CodigoCurso = cursoViewModel.CodigoCurso,
                    HorasPracticas = cursoViewModel.HorasPracticas,
                    Archivado = true
                };

                db.Entry(curso).State = EntityState.Modified;
                db.SaveChanges();

            }

            //Renvia a la vista de creación con los datos del Curso para editar
            return RedirectToAction("Lista", "Cursos");
        }

        public ActionResult Editar(int id)
        {
            // Obtener el curso de la base de datos utilizando el ID
            var curso = db.Cursos.Find(id);

            //crear modelo para enviar a la vista de edición/creacion
            CursoViewModel cursoViewModel = new CursoViewModel
            {
                Id = id,
                Titulo = curso.Titulo,
                CodigoCurso = curso.CodigoCurso,
                HorasPracticas = curso.HorasPracticas
            };


            //Renvia a la vista de creación con los datos del Curso para editar
            return View("Create", cursoViewModel);
        }

        public ActionResult ListaArchivados()
        {
            // Obtener todas las cohortes de la base de datos
            List<Curso> cursos = db.Cursos.Where(c => c.Archivado == false).ToList();

            CursoViewModel cursoViewModel = new CursoViewModel();
            cursoViewModel.Cursos = cursos;

            return View("Lista",cursoViewModel);

        }
    }
}