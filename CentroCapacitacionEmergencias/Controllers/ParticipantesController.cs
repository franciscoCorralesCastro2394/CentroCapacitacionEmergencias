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
    public class ParticipantesController : Controller
    {
        // Instancia del contexto de la base de datos
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Participantes
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(ParticipanteViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el correo ya existe en la base de datos
                bool correoExiste = db.Participantes
                    .Any(p => p.Correo == model.Correo);

                // Verificar si la cédula ya existe en la base de datos
                bool cedulaExiste = db.Participantes
                    .Any(p => p.Identificacion == model.Identificacion);

                if (correoExiste)
                {
                    ModelState.AddModelError("Correo", "Este correo ya está registrado");
                    return View(model);
                }

                if (cedulaExiste)
                {
                    ModelState.AddModelError("Identificacion", "Esta identificación ya existe");
                    return View(model);
                }

                // Crear un nuevo participante a partir del modelo de vista
                Participante participante = new Participante
                {
                    TipoIdentificacion = model.TipoIdentificacion,
                    Identificacion = model.Identificacion,
                    NombreCompleto = model.NombreCompleto,
                    FechaNacimiento = model.FechaNacimiento,
                    Provincia = model.Provincia,
                    Canton = model.Canton,
                    Distrito = model.Distrito,
                    DetalleDireccion = model.DetalleDireccion,
                    EstadoCivil = model.EstadoCivil,
                    Correo = model.Correo,
                    Telefono = model.Telefono,
                    DireccionResidencia = model.DireccionResidencia,
                    ContactoEmergencia = model.ContactoEmergencia
                };

                db.Participantes.Add(participante);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}