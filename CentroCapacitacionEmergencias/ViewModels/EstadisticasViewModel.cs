using CentroCapacitacionEmergencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.ViewModels
{
    public class EstadisticasViewModel
    {

        public string Participante { get; set; }
        public string Curso { get; set; }
        public string Destreza { get; set; }
        public int TiempoRespuesta { get; set; }
        public int PuntajeFinal { get; set; }

        // Regla automática
        public bool TieneFallaCritica { get; set; }

        // Auditoría
        public bool AprobadoPorInstructor { get; set; }
        public string InstructorNombre { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}