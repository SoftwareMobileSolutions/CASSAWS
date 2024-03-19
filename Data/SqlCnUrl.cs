using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASSAWS.Data
{
    public class SqlCnUrl
    {
        public string getCon()
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("configuracion.json", optional: true, reloadOnChange: true);

            IConfiguration _configuration = builder.Build();
            string sqlCon = _configuration.GetConnectionString("cn");
            return sqlCon;
        }
    }
}
