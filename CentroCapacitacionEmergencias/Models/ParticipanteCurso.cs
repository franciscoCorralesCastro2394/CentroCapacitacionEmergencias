using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.Models
{
    public class ParticipanteCurso
    {
        public int Id { get; set; }

        public int ParticipanteId { get; set; }
        public int CursoId { get; set; }

        public virtual Participante Participante { get; set; }
        public virtual Curso Curso { get; set; }
    }
}