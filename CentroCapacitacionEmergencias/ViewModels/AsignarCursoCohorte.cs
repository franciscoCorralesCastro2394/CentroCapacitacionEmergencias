using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentroCapacitacionEmergencias.ViewModels
{
    public class AsignarCursoCohorte
    {
        public int ParticipanteId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un curso al menos")]
        public List<int> CursosSeleccionados { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Cohorte")]
        public int CohorteId { get; set; }

        public string NombreParticipante { get; set; }

        public IEnumerable<SelectListItem> Cursos { get; set; }

        public IEnumerable<SelectListItem> Cohortes { get; set; }
    }
}