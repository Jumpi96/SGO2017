using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGO.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
    }
}
