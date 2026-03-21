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


        public ActionResult ListaParticipantes(ParticipanteViewModel participanteViewModel, int? detalleId, int? evaluacionId,int? CursoIdEvaluar)
        {

            // Cargar las cohortes y cursos para los filtros en la vista de lista de participantes Sin archivar
            participanteViewModel.Cohortes = db.Cohortes.Where(c => c.Archivado == true)
                                                .Select(c => new SelectListItem
                                                {
                                                    Value = c.Id.ToString(),
                                                    Text = c.Nombre
                                                }).ToList();

            // Cargar las cohortes y cursos para los filtros en la vista de lista de participantes Sin archivar
            participanteViewModel.Cursos = db.Cursos.Where(c => c.Archivado == true)
                                              .Select(c => new SelectListItem
                                              {
                                                  Value = c.Id.ToString(),
                                                  Text = c.Titulo
                                              }).ToList();



            // Consulta base para obtener los participantes activos
            var query = db.Participantes
                        .Where(p => p.Estado);

            // Aplicar filtros si se proporcionan en el modelo de vista
            if (!string.IsNullOrEmpty(participanteViewModel.filtroNombre))
            {
                // Filtrar por nombre (búsqueda parcial)
                query = query.Where(p => p.NombreCompleto.Contains(participanteViewModel.filtroNombre));
            }

            // Filtrar por cédula (búsqueda parcial)
            if (!string.IsNullOrEmpty(participanteViewModel.filtroCedula))
            {
                query = query.Where(p => p.Identificacion.Contains(participanteViewModel.filtroCedula));

            }

            // Filtrar por cohorte
            if (participanteViewModel.filtroCohorteId != null)
            {
                query = query.Where(p => db.ParticipanteCohortes.Any(
                     pc => pc.ParticipanteId == p.Id && pc.CohorteID == participanteViewModel.filtroCohorteId && pc.Cohorte.Archivado == true
                    ));
            }

            // Filtrar por curso
            if (participanteViewModel.filtroCursoId != null)
            {
                query = query.Where(p => db.ParticipanteCursos.Any(
                     pc => pc.ParticipanteId == p.Id && pc.CursoId == participanteViewModel.filtroCursoId && pc.Curso.Archivado == true
                    ));
            }

            // Si se proporciona un ID de detalle, buscar el participante correspondiente y sus cursos
            if (detalleId != null)
            {
                //obtiene los cursos de un participante
                var cursos = db.ParticipanteCursos.
                Where(pc => pc.ParticipanteId == participanteViewModel.detalleId && pc.Curso.Archivado == true).
                Select(pc => pc.Curso).
                ToList();

                //obtiene los cohortes de un participante
                var cohortes = db.ParticipanteCohortes.
                Where(pc => pc.ParticipanteId == detalleId && pc.Cohorte.Archivado == true).
                Select(pc => pc.Cohorte).
                ToList();


                ViewBag.Cohortes = cohortes;

                ViewBag.Cursos = cursos;
                // Si se proporciona un ID de detalle, buscar el participante correspondiente
                ViewBag.ParticipanteDetalle = db.Participantes.Find(participanteViewModel.detalleId);

            }

            // Si se proporciona un ID de evaluación, buscar el participante correspondiente y los cursos para evaluar
            if (evaluacionId != null)
            {
                //obtiene los cursos en los que se registro un estudiante paar evaluar
                var cursosSelectList = db.ParticipanteCursos
                                .Where(pc => pc.ParticipanteId == evaluacionId && pc.Curso.Archivado == true)
                                .Select(pc => new SelectListItem
                                {
                                    Value = pc.Curso.Id.ToString(),
                                    Text = pc.Curso.Titulo
                                })
                                .ToList();


                participanteViewModel.CursosPorEvaluar = cursosSelectList;

                ViewBag.ParticipanteEvaluar = db.Participantes.Find(participanteViewModel.evaluacionId);
                
                participanteViewModel.ParticipanteEvaluado = db.Participantes.Find(participanteViewModel.evaluacionId).NombreCompleto;

            }

            if (CursoIdEvaluar != null) {
             
                //obtiene los cursos de un participante
                var destrezas = db.CursoDestrezas
                            .Where(cd => cd.CursoId == CursoIdEvaluar)
                            .Select(cd => cd.Destreza)
                            .ToList();

                ViewBag.destrezas = destrezas;
                participanteViewModel.destrezasParticipante = destrezas;

                participanteViewModel.CursoTituloEvaluar = db.Cursos.Find(participanteViewModel.CursoIdEvaluar).Titulo;

            }

            // Ejecutar la consulta y obtener la lista de participantes filtrada
            participanteViewModel.Participantes = query.ToList();

            return View(participanteViewModel);
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

                //TempData["Mensaje"] = "Participante creado correctamente.";
                return RedirectToAction("Asignar","Asignar", new { id = participante.Id});

            }

            return View(model);
        }

        public ActionResult Editar(int id)
        {
            // Buscar el participante por su ID
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

        public ActionResult RemoverCohorte(int idCohorte, int idParticipante) 
        {
            //
            var relaciones = db.ParticipanteCohortes
                            .Where(pc => pc.ParticipanteId == idParticipante && pc.CohorteID == idCohorte)
                            .ToList();

            if (relaciones != null) 
            {
                db.ParticipanteCohortes.RemoveRange(relaciones);

                db.SaveChanges();
            }

            return RedirectToAction("ListaParticipantes");
        }

        public ActionResult RemoverCurso(int idCurso, int idParticipante)
        {
            //
            var relaciones = db.ParticipanteCursos
                            .Where(pc => pc.ParticipanteId == idParticipante && pc.CursoId == idCurso)
                            .ToList();

            if (relaciones != null)
            {
                db.ParticipanteCursos.RemoveRange(relaciones);

                db.SaveChanges();
            }

            return RedirectToAction("ListaParticipantes");
        }

        public ActionResult Evaluar(int? cursoId)
        {
            

            // Lista de destrezas
            var destrezas = new List<Destreza>();

            if (cursoId.HasValue && cursoId > 0)
            {
                destrezas = db.CursoDestrezas
                    .Where(cd => cd.CursoId == cursoId)
                    .Select(cd => cd.Destreza)
                    .ToList();
            }

            return View(destrezas);
        }
    }
}