using CentroCapacitacionEmergencias.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.SecurityConfig
{
    public static class SecurityConfig
    {
        public static LoginSecuritySettings GetSettings()
        {
            var json = File.ReadAllText(
                System.Web.Hosting.HostingEnvironment.MapPath("~/securitySettings.json")
            );

            dynamic config = JsonConvert.DeserializeObject(json);

            return new LoginSecuritySettings
            {
                MaxFailedAttempts = config.LoginSecurity.MaxFailedAttempts,
                LockMinutes = config.LoginSecurity.LockMinutes
            };
        }

    }
}