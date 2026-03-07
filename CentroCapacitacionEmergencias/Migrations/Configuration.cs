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

                var InstructorRole = context.Roles.First(r => r.Nombre == "Instructor");

                context.Usuarios.Add(new Usuario
                {
                    Nombre = "Instructor",
                    Apellido = "Sistema",
                    Correo = "Instructor@centro.com",
                    PasswordHash = ComputeSha256Hash("1234"),
                    RolId = InstructorRole.Id,
                    Activo = true
                });


            }

            // Aquí podrías agregar datos de ejemplo para Participantes, Cohortes y Cursos si lo deseas
            if (!context.Cohortes.Any())
            {
                context.Cohortes.Add(
                    new Cohorte
                    {
                        Nombre = "Cohorte 2025-A",
                        FechaInicio = new DateTime(2025, 1, 10),
                        FechaFin = new DateTime(2025, 4, 30)
                    });

                context.Cohortes.Add(

                new Cohorte
                    {
                        Nombre = "Cohorte 2025-B",
                        FechaInicio = new DateTime(2025, 5, 10),
                        FechaFin = new DateTime(2025, 8, 30)
                    });

                context.Cohortes.Add(

                    new Cohorte
                    {
                        Nombre = "Cohorte 2025-C",
                        FechaInicio = new DateTime(2025, 9, 1),
                        FechaFin = new DateTime(2025, 12, 15)
                    });

            }

            if (!context.Cursos.Any())
            {
                context.Cursos.Add(
                        new Curso
                        {
                            Titulo = "Trauma Avanzado",
                            CodigoCurso = "TA-101",
                            HorasPracticas = 40
                        });

                context.Cursos.Add(
                        new Curso
                        {
                            Titulo = "Soporte Vital Básico",
                            CodigoCurso = "SVB-201",
                            HorasPracticas = 30
                        });

                context.Cursos.Add(
                        new Curso
                                {
                                    Titulo = "Rescate en Montaña",
                                    CodigoCurso = "RM-305",
                                    HorasPracticas = 50
                                });

               context.Cursos.Add(
                       new Curso
                                {
                                    Titulo = "Atención Prehospitalaria",
                                    CodigoCurso = "APH-150",
                                    HorasPracticas = 45
                                });
            }

            if (!context.Participantes.Any()) 
            {
                  context.Participantes.Add(
                        new Participante
                        {
                            TipoIdentificacion = "Cedula",
                            Identificacion = "101230456",
                            NombreCompleto = "Carlos Mora Perez",
                            FechaNacimiento = new DateTime(1995, 3, 12),
                            Provincia = "San Jose",
                            Canton = "Perez Zeledon",
                            Distrito = "San Isidro",
                            DetalleDireccion = "Barrio Cooperativa",
                            EstadoCivil = "Soltero",
                            Correo = "carlos.mora@email.com",
                            Telefono = "88887777"
                        });

                        context.Participantes.Add(
                        new Participante
                        {
                            TipoIdentificacion = "Cedula",
                            Identificacion = "102340567",
                            NombreCompleto = "Daniela Vargas Soto",
                            FechaNacimiento = new DateTime(1998, 7, 22),
                            Provincia = "Alajuela",
                            Canton = "San Carlos",
                            Distrito = "Quesada",
                            DetalleDireccion = "300m norte iglesia",
                            EstadoCivil = "Soltero",
                            Correo = "daniela.vargas@email.com",
                            Telefono = "88990011"
                        });
            }

            context.SaveChanges();
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
