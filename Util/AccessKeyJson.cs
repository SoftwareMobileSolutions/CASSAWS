using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.Json;
using System.IO;
using CASSAWS.Models;

namespace CASSAWS.Util
{
    public class configJson
    {
     
        public configUserModel getConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("configuracion.json", optional: true, reloadOnChange: true);

            IConfiguration config = builder.Build();

            var datos = config.GetSection("datosLoginRequest").Get<configUserModel>(); ;

            return datos;
        }
    }
}
