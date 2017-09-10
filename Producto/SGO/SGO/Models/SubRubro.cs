using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGO.Models
{
    public class SubRubro
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public virtual Rubro Rubro { get; set; } 

    }
}
