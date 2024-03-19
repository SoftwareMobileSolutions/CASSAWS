using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASSAWS.Models
{
    public class configUserModel
    {
        public string user { get; set; }
        public string password { get; set; }
        public string urlRequest { get; set; }
        public string urlRequest_setData { get; set; }
        public string method { get; set; }
        public string contentType { get; set; }
        public int timeout { get; set; }
    }
}
