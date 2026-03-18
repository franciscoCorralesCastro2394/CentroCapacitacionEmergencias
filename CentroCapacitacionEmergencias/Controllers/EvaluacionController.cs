using CentroCapacitacionEmergencias.Data;
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
                                                 select pc).ToList();






            return View(evaluacionViewModel);
        } 
    }
}