using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentroCapacitacionEmergencias.ViewModels
{
    public class CursoViewModel
    {
        public string Titulo { get; set; }

        public string CodigoCurso { get; set; }

        public int HorasPracticas { get; set; }

        public List<int> InstructoresSeleccionados { get; set; }

        public List<int> CohortesSeleccionadas { get; set; }

        public List<SelectListItem> Instructores { get; set; }

        public List<SelectListItem> Cohortes { get; set; }
    }
}