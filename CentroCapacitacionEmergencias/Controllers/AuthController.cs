using CentroCapacitacionEmergencias.Data;
using CentroCapacitacionEmergencias.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentroCapacitacionEmergencias.Controllers
{
    public class AuthController : Controller
    {
        // Instancia del contexto de la base de datos
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            // Obtener configuración de seguridad (ejemplo, no se usa en este código)
            var config = SecurityConfig.SecurityConfig.GetSettings();
           

             string hashedPassword = ComputeSha256Hash(model.Password);
             var userAuth = db.Usuarios
                             .Include("Rol")
                             .FirstOrDefault(u => u.Correo == model.Correo
                                               && u.PasswordHash == hashedPassword
                                               && u.Activo);
            var userExist = db.Usuarios
                             .Include("Rol")
                             .FirstOrDefault(u => u.Correo == model.Correo
                                               && u.Activo);

            if (userAuth == null && userExist == null)
            {
                //  No existe con esas credenciales o el usuario está inactivo
                ModelState.AddModelError("", "Credenciales inválidas");
                return View(model);
            }
            else {

                // Verificar si el usuario está bloqueado
                if (userExist.BloqueadoHasta != null && userExist.BloqueadoHasta > DateTime.Now)
                {
                    ModelState.AddModelError("", "Usuario bloqueado temporalmente.");
                    return View(model);
                }

                if (userAuth != null && userAuth.PasswordHash == hashedPassword && userExist !=null)
                {
                    userAuth.IntentosFallidos = 0;
                    db.SaveChanges();
                    Session["UserId"] = userAuth.Id;
                    Session["UserName"] = userAuth.Nombre;
                    Session["UserRole"] = userAuth.Rol.Nombre;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    userExist.IntentosFallidos++;

                    if (userExist.IntentosFallidos >= config.MaxFailedAttempts)
                    {
                        userExist.BloqueadoHasta = DateTime.Now.AddMinutes(config.LockMinutes);
                        userExist.IntentosFallidos = 0; // Reiniciar contador de intentos
                        db.SaveChanges();
                        ModelState.AddModelError("", "Usuario bloqueado temporalmente debido a múltiples intentos fallidos.");
                    }
                    db.SaveChanges();

                    ModelState.AddModelError("", "Usuario o contraseña incorrectos");

                }

            }
          


            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (var sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawData));
                var builder = new System.Text.StringBuilder();
                foreach (byte t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}