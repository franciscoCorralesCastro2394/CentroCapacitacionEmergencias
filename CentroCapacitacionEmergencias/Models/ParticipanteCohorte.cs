using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.Models
{
    public class ParticipanteCohorte
    {
        public int Id { get; set; }

        public int ParticipanteId { get; set; }
        public int CohorteID { get; set; }

        public virtual Participante Participante { get; set; }
        public virtual Cohorte Cohorte { get; set; }

    }
}