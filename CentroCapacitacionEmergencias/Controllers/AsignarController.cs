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
    

    public class AsignarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Asignar
        public ActionResult Index()
        {
            return View();
        }

        public AsignarCursoCohorte crearModelo(int id) 
        {
            Participante participante = db.Participantes.Find(id);

            return new AsignarCursoCohorte
            {
                ParticipanteId = participante.Id,
                NombreParticipante = participante.NombreCompleto,
                Cursos = db.Cursos.
                Where(c => c.Archivado == true ).
                Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Titulo
                }),
                Cohortes = db.Cohortes.
                Where(c => c.Archivado == true ).
                Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                })
            };
        }

        public ActionResult Asignar(int id)
        {
            //Participante participante = db.Participantes.Find(id);

            //crea el view model con las listas y la información necesaria 
            AsignarCursoCohorte asignarCursoCohorte = crearModelo(id);

            return View(asignarCursoCohorte);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Asignar(AsignarCursoCohorte asignarCursoCohorte)
        {
            if (asignarCursoCohorte.CursosSeleccionados != null && asignarCursoCohorte.CohorteId != 0)
            {

                // asignar cursos al participante 
                foreach (var cursoId in asignarCursoCohorte.CursosSeleccionados)
                {
                    var asignacion = new ParticipanteCurso
                    {
                        ParticipanteId = asignarCursoCohorte.ParticipanteId,
                        CursoId = cursoId,
                        CohorteId = asignarCursoCohorte.CohorteId
                    };

                    db.ParticipanteCursos.Add(asignacion);
                }
                //asigna un cohorte al participante
                ParticipanteCohorte participanteCohorte = new ParticipanteCohorte
                {
                    ParticipanteId = asignarCursoCohorte.ParticipanteId,
                    CohorteID = asignarCursoCohorte.CohorteId
                };

                db.ParticipanteCohortes.Add(participanteCohorte);

                db.SaveChanges();

                return RedirectToAction("ListaParticipantes", "Participantes");

            }
            else 
            {
                //TempData["Error"] = "Errores en asignar Cursos";
                return View(crearModelo(asignarCursoCohorte.ParticipanteId));

            }

            //return View(asignarCursoCohorte);

        }


    }
}