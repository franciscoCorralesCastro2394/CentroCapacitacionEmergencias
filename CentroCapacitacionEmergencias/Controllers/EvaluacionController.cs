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
    public class EvaluacionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Evaluacion
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Evaluacion(EvaluacionViewModel evaluacionViewModel) 
        {
            // Aquí puedes realizar la lógica para procesar la evaluación, como guardar los datos en la base de datos o realizar cálculos.
            evaluacionViewModel.Curso = db.Cursos.Find(evaluacionViewModel.idCurso);
            evaluacionViewModel.Participante = db.Participantes.Find(evaluacionViewModel.idParticipante);
            evaluacionViewModel.Destreza = db.Destrezas.Find(evaluacionViewModel.idDestreza);
            evaluacionViewModel.PuntoControls = (from pcd in db.PuntoControlDestreza
                                                 join pc in db.PuntoControls
                                                 on pcd.IdPunto equals pc.Id
                                                 where pcd.IdDestreza == evaluacionViewModel.idDestreza
                                                 select pc).Distinct().ToList();


            evaluacionViewModel.Puntos = new Dictionary<int, bool>();

            return View(evaluacionViewModel);
        }

        [HttpPost]
        public ActionResult GuardarEvaluacion(EvaluacionViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var puntos = Request.Form.AllKeys
                            .Where(k => k.StartsWith("Puntos["))
                            .ToDictionary(
                                k => int.Parse(k.Replace("Puntos[", "").Replace("]", "")),
                                k => Request.Form[k] == "true"
                            );


            // Convertir tiempo
            var partes = model.Tiempo.Split(':');
            int tiempoSegundos = (int.Parse(partes[0]) * 60) + int.Parse(partes[1]);

            // Validar puntos críticos si algún punto es falso, se considera una falla crítica
            bool fallaCritica = !puntos.Any(p => p.Value == false);

            int puntajeFinal = model.Puntaje;


            // Regla del 70%
            if (fallaCritica && puntajeFinal > 70)
            {
                puntajeFinal = 70;
            }

            var evaluacion = new EvaluacionDestreza
            {
                Curso = db.Cursos.Find(model.CursoId),
                Participante = db.Participantes.Find(model.ParticipanteId),
                Destreza = db.Destrezas.Find(model.DestrezaId),
                ParticipanteId = model.idParticipante,
                CursoId = model.idCurso,
                DestrezaId = model.idDestreza,
                TiempoRespuesta = tiempoSegundos,
                PuntajeFinal = puntajeFinal,
                TieneFallaCritica = fallaCritica,
                AprobadoPorInstructor = model.AprobadoPorInstructor,
                FechaRegistro = DateTime.Now,
                InstructorNombre = Session["UserName"].ToString(),
                InstructorId = (int)Session["UserId"]
            };

            db.EvaluacionDestreza.Add(evaluacion);
            db.SaveChanges();


            return RedirectToAction("ListaParticipantes", "Participantes");
        }
    }
}