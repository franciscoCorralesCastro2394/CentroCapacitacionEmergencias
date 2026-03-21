using CentroCapacitacionEmergencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentroCapacitacionEmergencias.ViewModels
{
    public class CursoViewModel
    {
        public int Id { get; set; } = 0;
        public string Titulo { get; set; }

        public string CodigoCurso { get; set; }

        public int HorasPracticas { get; set; }

        public int? maxInstructores { get; set; } = 1;

        public List<int> InstructoresSeleccionados { get; set; }

        public List<int> CohortesSeleccionadas { get; set; }

        public List<SelectListItem> Instructores { get; set; }

        public List<SelectListItem> Cohortes { get; set; }

        public List<Curso> Cursos { get; set; }
    }
}