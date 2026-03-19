using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.Models
{
    public class EvaluacionDestreza
    {
        public int Id { get; set; }

        public int ParticipanteId { get; set; }
        public int CursoId { get; set; }
        public int DestrezaId { get; set; }

        public int InstructorId { get; set; }

        public int TiempoRespuesta { get; set; }
        public int PuntajeFinal { get; set; }

        // Regla automática
        public bool TieneFallaCritica { get; set; }

        // Auditoría
        public bool AprobadoPorInstructor { get; set; }
        public string InstructorNombre { get; set; }

        public DateTime FechaRegistro { get; set; }

        public virtual Participante Participante { get; set; }
        public virtual Curso Curso { get; set; }
        public virtual Destreza Destreza { get; set; }

    }
}