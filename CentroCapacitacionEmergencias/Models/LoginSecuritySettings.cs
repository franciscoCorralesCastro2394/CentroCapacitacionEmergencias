using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.Models
{
    public class LoginSecuritySettings
    {
        public int MaxFailedAttempts { get; set; }
        public int LockMinutes { get; set; }
    }
}