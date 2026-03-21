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
    public class CohortesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cohortes
        public ActionResult Create()
        {
            return View("Create", new CohorteViewModel { 
            Cohorte = new Cohorte
            {
                Id = 0
            }
            });
        }

        [HttpPost]
        public ActionResult Create(CohorteViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Validar que no exista una cohorte con el mismo nombre
                bool existe = db.Cohortes.Any(c => c.Nombre == model.Cohorte.Nombre);

                if (existe)
                {
                    ModelState.AddModelError("Nombre", "Ya existe una cohorte con ese nombre");
                    return View(model);
                }

                // Validar que la fecha fin sea mayor a la fecha inicio
                if (model.Cohorte.FechaFin <= model.Cohorte.FechaInicio)
                {
                    ModelState.AddModelError("", "La fecha fin debe ser mayor a la fecha inicio");
                    return View(model);
                }

                // Si el modelo es válido, guardar la cohorte en la base de datos con estado Archivado en true
                model.Cohorte.Archivado = true;
                db.Cohortes.Add(model.Cohorte);
                db.SaveChanges();

                //debe redireccionar a otro lugar 
                return RedirectToAction("Lista");
            }

            return View(model);
        }

        public ActionResult Lista() 
        {

            // Obtener todas las cohortes de la base de datos ademas cohortes que ya cerraron no son listados 
            List<Cohorte> cohortes = db.Cohortes.Where(c => c.Archivado == true && c.FechaFin > DateTime.Now).ToList();

            CohorteViewModel cohorteViewModel = new CohorteViewModel();
            cohorteViewModel.Cohortes = cohortes;

            return View(cohorteViewModel);
        }


        public ActionResult ListaArchivados()
        {
            // Obtener todas las cohortes de la base de datos ademas cohortes que ya cerraron no son listados
            List<Cohorte> cohortes = db.Cohortes.Where(c => c.Archivado == false && c.FechaFin >  DateTime.Now).ToList();

            CohorteViewModel cohorteViewModel = new CohorteViewModel();
            cohorteViewModel.Cohortes = cohortes;

            return View("Lista",cohorteViewModel);

        } 

        public ActionResult Delete(int id)
        {
            Cohorte cohorte = db.Cohortes.Find(id);
            bool tieneActivos = db.ParticipanteCohortes
                                   .Any(pc => pc.CohorteID == id && pc.Participante.Estado);

            // Si el cohorte tiene participantes activos, no permitir la eliminación
            if (tieneActivos && cohorte.Archivado == true)
            {
                TempData["Error"] = "No se puede editar la cohorte porque tiene participantes activos.";
                return RedirectToAction("Lista");
            }
            

            if (cohorte != null)
            {
                // Cambiar el estado de Archivado a false para marcarlo como archivado en lugar de eliminarlo físicamente
                //Si estaba archivado lo desarchiva y viceversa
                cohorte.Archivado = !cohorte.Archivado;
                db.SaveChanges();
            }


            return RedirectToAction("Lista", "Cohortes");

        }


        public ActionResult Editar(int id)
        {

            bool tieneActivos = db.ParticipanteCohortes
                                    .Any(pc => pc.CohorteID == id && pc.Participante.Estado);
            // Si el cohorte tiene participantes activos, no permitir la edición
            if (!tieneActivos)
            {
                // Obtener el cohorte de la base de datos utilizando el ID
                Cohorte cohorte = db.Cohortes.Find(id);

                //crear modelo para enviar a la vista de edición/creacion
                CohorteViewModel cohorteViewModel = new CohorteViewModel();

                cohorteViewModel.Cohorte = new Cohorte
                {
                    Id = id,
                    Nombre = cohorte.Nombre,
                    FechaInicio = cohorte.FechaInicio,
                    FechaFin = cohorte.FechaFin
                };

                //Renvia a la vista de creación con los datos del cohorte para editar
                return View("Create", cohorteViewModel);

            }
            else 
            {
                TempData["Error"] = "No se puede editar la cohorte porque tiene participantes activos. Puede archivarla.";
                return RedirectToAction("Lista");
            }

         
        }

        public ActionResult EditarCohorte(CohorteViewModel cohorteView)
        {
            //
            Cohorte cohorteEditado = cohorteView.Cohorte;


            if (cohorteEditado.Id != 0)
            {

                Cohorte cohorte = new Cohorte
                {
                    Id = cohorteEditado.Id,
                    Nombre = cohorteEditado.Nombre,
                    FechaInicio = cohorteEditado.FechaInicio,
                    FechaFin = cohorteEditado.FechaFin
                };

                db.Entry(cohorte).State = EntityState.Modified;
                db.SaveChanges();

            }

            //Renvia a la vista de creación con los datos del Curso para editar
            return RedirectToAction("Lista", "Cohortes");
        }
    }
}