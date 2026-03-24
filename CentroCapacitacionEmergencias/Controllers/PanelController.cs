using Antlr.Runtime.Tree;
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
    public class PanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Panel
        public ActionResult Index(int? idCurso, int? idCohorte)
        {
            int intructorId = (int)Session["UserId"];

            //Cohortes
            List<EvaluacionDestreza> evaluacionCohortes = db.EvaluacionDestreza.Where(e => e.CursoId == 
                                                                 idCurso && e.InstructorId == intructorId
                                                                 ).ToList();
            ViewBag.EvaluacionCohortes = evaluacionCohortes;


            var cursos = db.Cursos.Where(c => c.Id == idCurso).ToList();

            ViewBag.Cursos = cursos;

            var cohorte = db.Cohortes.Where(c => c.Id == idCohorte).ToList();
            ViewBag.Cohorte = cohorte;

            // Participantes 
            List<Participante> participantesporCurso = db.ParticipanteCursos.Where(pc => pc.CursoId == idCurso)
                                        .Select(pc => pc.Participante)
                                        .ToList();

            ViewBag.ParticipantesCurso = participantesporCurso;
            // Participantes Cohorte
            List<Participante> participantesCohorte = db.ParticipanteCohortes.Where(pc => pc.CohorteID == idCohorte)
                                        .Select(pc => pc.Participante)
                                        .ToList();
            ViewBag.ParticipantesCohorte = participantesCohorte;

            // Evaluaciones 
            int evaluacionesTotales = evaluacionCohortes.Count();
            ViewBag.EvaluacionesTotales = evaluacionesTotales;

            
            int putajeMinimo = 70; // Ejemplo de puntaje mínimo para aprobar
            ViewBag.PuntajeMinimo = putajeMinimo;

            // Evaluaciones aprobadas
            var aprobadas = evaluacionCohortes.Where(e => e.PuntajeFinal >= putajeMinimo).ToList();
            ViewBag.Aprobadas = aprobadas;

            // Tasa de aprobación
            double tasa = evaluacionesTotales > 0 ? (double)aprobadas.Count() / evaluacionesTotales * 100 : 0;
            ViewBag.TasaAprobacion = tasa;
            ///

            int certificados = aprobadas.Count();
            ViewBag.Certificados = certificados;

            int horas = 0;

            // Horas prácticas
            foreach (var item in evaluacionCohortes)
            {
                var curso = db.Cursos.FirstOrDefault(c => c.Id == item.CursoId);
                if (curso != null)
                {
                    horas += curso.HorasPracticas;
                }
            }

            ViewBag.Horas = horas;


            // Destrezas 
            List<Destreza>  destrezas = new List<Destreza>();
            List<PuntoControl> puntos = new List<PuntoControl>();
            foreach (var item in evaluacionCohortes)
            {
                var deztreza = db.Destrezas.FirstOrDefault(d => d.Id == item.DestrezaId);
                if (deztreza != null)
                {
                    destrezas.Add(deztreza);
                    PuntoControlDestreza puntoControlDestreza = db.PuntoControlDestreza.
                        FirstOrDefault(pcd => pcd.IdDestreza == deztreza.Id);
                    if (puntoControlDestreza != null) {

                        if (!puntos.Contains(db.PuntoControls.Find(puntoControlDestreza.IdPunto)))
                        {
                            puntos.Add(db.PuntoControls.Find(puntoControlDestreza.IdPunto));
                        }

                    }
                }
            }
            ViewBag.Destrezas = destrezas;
            ViewBag.Puntos = puntos;

            return View();
        }

        public ActionResult Seleccion() 
        {

            int intructorId = (int)Session["UserId"];

            List<Curso> cursosinstructor = db.Cursos.Where(c => c.Instructores.Any(i => i.Id == intructorId)).ToList();


            // Obtener los cursos asociados al instructor
            var cursos = cursosinstructor.Select(c => new SelectListItem { 
                                       Value = c.Id.ToString(),
                                       Text = c.Titulo
                                       }).ToList();

            // Obtener las cohortes asociadas a los cursos del instructor
            var cohortes = cursosinstructor.SelectMany(c => c.Cohortes).
                                            Select(co => new SelectListItem
                                            {
                                                Value = co.Id.ToString(),
                                                Text = co.Nombre

                                            }).ToList();




            ViewBag.Cursos = cursos;


            ViewBag.Cohortes = cohortes;

            return View();

        }
    }
}