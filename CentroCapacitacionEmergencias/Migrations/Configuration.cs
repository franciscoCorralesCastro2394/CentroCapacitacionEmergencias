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

                context.Usuarios.Add(new Usuario
                {
                    Nombre = "Instructor2",
                    Apellido = "Sistema2",
                    Correo = "Instructor2@centro.com",
                    PasswordHash = ComputeSha256Hash("1234"),
                    RolId = InstructorRole.Id,
                    Activo = true
                });

                context.Usuarios.Add(new Usuario
                {
                    Nombre = "Instructor3",
                    Apellido = "Sistema3",
                    Correo = "Instructor3@centro.com",
                    PasswordHash = ComputeSha256Hash("1234"),
                    RolId = InstructorRole.Id,
                    Activo = true
                });

                context.Usuarios.Add(new Usuario
                {
                    Nombre = "Instructor4",
                    Apellido = "Sistema4",
                    Correo = "Instructor4@centro.com",
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
                            Telefono = "88887777",
                            Estado = true
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
                            Telefono = "88990011",
                            Estado = true
                        });
            }

            if (!context.ParticipanteCursos.Any()) 
            {
                context.ParticipanteCursos.Add(new ParticipanteCurso
                {
                    ParticipanteId = 1,
                    CursoId = 1,
                    CohorteId = 1
                });
                context.ParticipanteCursos.Add(new ParticipanteCurso
                {
                    ParticipanteId = 1,
                    CursoId = 2,
                    CohorteId = 1
                });
                context.ParticipanteCursos.Add(new ParticipanteCurso
                {
                    ParticipanteId = 1,
                    CursoId = 2,
                    CohorteId = 1
                });
                context.ParticipanteCursos.Add(new ParticipanteCurso
                {
                    ParticipanteId = 2,
                    CursoId = 1,
                    CohorteId = 1
                });

            }

            if (!context.ParticipanteCohortes.Any())
            {
                context.ParticipanteCohortes.Add(
                    new ParticipanteCohorte
                    {
                        CohorteID = 1,
                        ParticipanteId = 1
                    }
                    );
            }

            if (!context.Destrezas.Any()) {

                context.Destrezas.Add(new Destreza { Titulo = "Evaluación primaria del paciente", Descripcion = "Valoración inicial ABC" });
                context.Destrezas.Add(new Destreza { Titulo = "RCP básica", Descripcion = "Reanimación cardiopulmonar en adultos" });
                context.Destrezas.Add(new Destreza { Titulo = "Uso de DEA", Descripcion = "Desfibrilador externo automático" });
                context.Destrezas.Add(new Destreza { Titulo = "Control de vía aérea básica", Descripcion = "Apertura y mantenimiento de vía aérea" });
                context.Destrezas.Add(new Destreza { Titulo = "Ventilación con bolsa-válvula-mascarilla", Descripcion = "Soporte ventilatorio manual" });

                context.Destrezas.Add(new Destreza { Titulo = "Control de hemorragias", Descripcion = "Uso de presión directa y torniquete" });
                context.Destrezas.Add(new Destreza { Titulo = "Inmovilización de extremidades", Descripcion = "Uso de férulas" });
                context.Destrezas.Add(new Destreza { Titulo = "Manejo de paciente politraumatizado", Descripcion = "Atención integral en trauma" });
                context.Destrezas.Add(new Destreza { Titulo = "Colocación de collar cervical", Descripcion = "Estabilización cervical" });
                context.Destrezas.Add(new Destreza { Titulo = "Extricación de paciente", Descripcion = "Rescate seguro en accidentes" });
                                    
            }

            if (!context.CursoDestrezas.Any())
            {
                context.CursoDestrezas.Add(new CursoDestrezas { CursoId = 1, DestrezaId = 1 });
                context.CursoDestrezas.Add(new CursoDestrezas { CursoId = 2, DestrezaId = 2 });
                context.CursoDestrezas.Add(new CursoDestrezas { CursoId = 3, DestrezaId = 3 });
            }

            if (!context.PuntoControls.Any()) { 
                context.PuntoControls.Add(new PuntoControl {Descripcion = "Verifica seguridad de la escena antes de intervenir" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Utiliza equipo de protección personal (EPP) correctamente" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Realiza evaluación primaria (ABC) de forma adecuada" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Identifica correctamente el estado de conciencia del paciente" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Activa el sistema de emergencias oportunamente" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Inicia maniobras dentro del tiempo esperado" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Aplica técnica correcta según el protocolo de la destreza" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Mantiene control de la vía aérea en todo momento" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Controla hemorragias de manera efectiva" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Realiza uso adecuado de los equipos disponibles" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Sigue la secuencia del protocolo sin omitir pasos críticos" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Mantiene comunicación clara con el equipo" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Toma decisiones adecuadas bajo presión" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Evita acciones que comprometan la seguridad del paciente" });
                context.PuntoControls.Add(new PuntoControl { Descripcion = "Finaliza el procedimiento conforme al protocolo establecido" });
            }

            if (!context.PuntoControlDestreza.Any()) 
            {
                context.PuntoControlDestreza.Add(new PuntoControlDestreza { IdDestreza = 1, IdPunto = 1 });
                context.PuntoControlDestreza.Add(new PuntoControlDestreza { IdDestreza = 1, IdPunto = 2 });
                context.PuntoControlDestreza.Add(new PuntoControlDestreza { IdDestreza = 1, IdPunto = 3 });
                context.PuntoControlDestreza.Add(new PuntoControlDestreza { IdDestreza = 2, IdPunto = 1 });
                context.PuntoControlDestreza.Add(new PuntoControlDestreza { IdDestreza = 3, IdPunto = 1 });
                context.PuntoControlDestreza.Add(new PuntoControlDestreza { IdDestreza = 4, IdPunto = 1 });
                context.PuntoControlDestreza.Add(new PuntoControlDestreza { IdDestreza = 5, IdPunto = 1 });
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
