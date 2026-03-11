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
    public class ParticipantesController : Controller
    {
        // Instancia del contexto de la base de datos
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Participantes
        public ActionResult Create()
        {
            //return View();
            return View("Create", new ParticipanteViewModel());
        }


        public ActionResult ListaParticipantes(int? detalleID,string nombre,string cedula)
        {

            if (!string.IsNullOrEmpty(nombre))
            {
                // Filtrar por nombre (búsqueda parcial)
                var participantesPorNombre = db.Participantes
                    .Where(p => p.Estado && p.NombreCompleto.Contains(nombre))
                    .ToList();
                return View(participantesPorNombre);
            }


            if (!string.IsNullOrEmpty(cedula))
            {
                // Filtrar por identificacion (búsqueda parcial)
                var participantesPorNombre = db.Participantes
                    .Where(p => p.Estado && p.Identificacion.Contains(cedula))
                    .ToList();
                return View(participantesPorNombre);
            }

            // Obtener solo los participantes activos (Estado = true)
            var participantes = db.Participantes.
                Where(p => p.Estado).
                ToList();


            // Si se proporciona un ID de detalle, buscar el participante correspondiente y sus cursos
            if (detalleID != null)
            {
                var cursos = db.ParticipanteCursos.
                Where(pc => pc.ParticipanteId == detalleID).
                Select(pc => pc.Curso).
                ToList();

                ViewBag.Cursos = cursos;
                // Si se proporciona un ID de detalle, buscar el participante correspondiente
                ViewBag.ParticipanteDetalle = db.Participantes.Find(detalleID);
            }


            return View(participantes);
        }

        public ActionResult CursosParticipante(int id)
        {
            var participante = db.Participantes.Find(id);

            var cursos = db.ParticipanteCursos
                           .Where(pc => pc.ParticipanteId == id)
                           .Select(pc => pc.Curso)
                           .ToList();

            //Pasar el participante y sus cursos a la vista dentro del ViewBag  
            ViewBag.Participante = participante;

            return View(cursos);
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
                    ContactoEmergencia = model.ContactoEmergencia,
                    Estado = true
                };

                db.Participantes.Add(participante);
                db.SaveChanges();

                TempData["Mensaje"] = "Participante creado correctamente.";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Editar(int id)
        {
            var participante = db.Participantes.Find(id);

            ParticipanteViewModel participanteViewModel = new ParticipanteViewModel
            {

                Canton = participante.Canton,
                ContactoEmergencia = participante.ContactoEmergencia,
                Provincia = participante.Provincia,
                Correo = participante.Correo,
                DetalleDireccion = participante.DetalleDireccion,
                DireccionResidencia = participante.DireccionResidencia,
                Distrito = participante.Distrito,
                EstadoCivil = participante.EstadoCivil,
                FechaNacimiento = participante.FechaNacimiento,
                Identificacion = participante.Identificacion,
                NombreCompleto = participante.NombreCompleto,
                Telefono = participante.Telefono,
                TipoIdentificacion = participante.TipoIdentificacion,
                Id = id
            };


            //Renvia a la vista de creación con los datos del participante para editar
            return View("Create", participanteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarParticipante(ParticipanteViewModel model)
        {

            if (ModelState.IsValid)
            {

                if (model.Id != 0)
                {

                    Participante participanteEditado = new Participante
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
                        ContactoEmergencia = model.ContactoEmergencia,
                        Estado = true,
                        Id = model.Id,
                    };

                    db.Entry(participanteEditado).State = EntityState.Modified;
                    db.SaveChanges();

                    //TempData["Mensaje"] = "Participante actualizado correctamente.";

                    return RedirectToAction("ListaParticipantes", "Participantes");
                }
            }

            return View("Create", model);
        }


        public ActionResult Delete(int id)
        {
            Participante participante = db.Participantes.Find(id);
            if (participante != null)
            {
                // En lugar de eliminar el registro, se marca como inactivo
                participante.Estado = false;
                db.SaveChanges();
            }
            return RedirectToAction("ListaParticipantes");
        }
    }
}