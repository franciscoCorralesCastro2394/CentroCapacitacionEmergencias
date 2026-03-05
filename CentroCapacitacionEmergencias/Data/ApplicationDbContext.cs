using CentroCapacitacionEmergencias.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("CentroEmergenciasConnection")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
    }
}