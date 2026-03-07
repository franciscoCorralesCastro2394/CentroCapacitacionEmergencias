using CentroCapacitacionEmergencias.Data;
using CentroCapacitacionEmergencias.Models;
using System;
using System.Collections.Generic;
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
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cohorte model)
        {
            if (ModelState.IsValid)
            {
                bool existe = db.Cohortes.Any(c => c.Nombre == model.Nombre);

                if (existe)
                {
                    ModelState.AddModelError("Nombre", "Ya existe una cohorte con ese nombre");
                    return View(model);
                }

                if (model.FechaFin <= model.FechaInicio)
                {
                    ModelState.AddModelError("", "La fecha fin debe ser mayor a la fecha inicio");
                    return View(model);
                }

                db.Cohortes.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}