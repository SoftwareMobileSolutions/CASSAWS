using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASSAWS.Models
{
    public class AliadosComunidadVelocidadModel
    {
        public string fechaEvento { get; set; }
        public string equipo { get; set; }
        public string codComunidad { get; set; }
        public float velocidad { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
    }
}
