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
    public class EstadisticasPartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EstadisticasPart
        public ActionResult EstadisticasPart(int id)
        {
            // Obtener todas las evaluaciones del participante
            List<EvaluacionDestreza> evaluaciones = db.EvaluacionDestreza.Where(e => e.ParticipanteId == id).ToList();

            // Crear una lista de ViewModel para mostrar en la vista
            List<EstadisticasViewModel> viewModel = new List<EstadisticasViewModel>();

            // Llenar el ViewModel con los datos de cada evaluación
            foreach (var eval in evaluaciones)
            {
                EstadisticasViewModel est = new EstadisticasViewModel
                {
                    Participante = db.Participantes.Find(id).NombreCompleto,
                    Curso = db.Cursos.Find(eval.CursoId).Titulo,
                    Destreza = db.Destrezas.Find(eval.DestrezaId).Titulo,
                    TiempoRespuesta = eval.TiempoRespuesta,
                    PuntajeFinal = eval.PuntajeFinal,
                    TieneFallaCritica = eval.TieneFallaCritica,
                    AprobadoPorInstructor = eval.AprobadoPorInstructor,
                    InstructorNombre = eval.InstructorNombre,
                    FechaRegistro = eval.FechaRegistro
                };

                viewModel.Add(est);
            }


            return View(viewModel);
        }


    }
}