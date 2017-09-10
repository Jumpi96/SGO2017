using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGO.Models
{
    public class Movimiento
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        public double Cantidad { get; set; }
        public virtual Receptor Receptor { get; set; }
        public virtual Usuario ModUsuario { get; set; }
        public DateTime ModFecha { get; set; }
        public virtual Obra Obra { get; set; }
        public virtual SubItem SubItem { get; set; }
    }
}
