namespace CentroCapacitacionEmergencias.Migrations
{
    using CentroCapacitacionEmergencias.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<CentroCapacitacionEmergencias.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CentroCapacitacionEmergencias.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any())
            {
                context.Roles.Add(new Rol { Nombre = "Administrador" });
                context.Roles.Add(new Rol { Nombre = "Instructor" });
                context.SaveChanges();
            }

            if (!context.Usuarios.Any())
            {
                var adminRole = context.Roles.First(r => r.Nombre == "Administrador");

                context.Usuarios.Add(new Usuario
                {
                    Nombre = "Admin",
                    Apellido = "Sistema",
                    Correo = "admin@centro.com",
                    PasswordHash = ComputeSha256Hash("1234"),
                    RolId = adminRole.Id,
                    Activo = true
                });

                context.SaveChanges();
            }


        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
