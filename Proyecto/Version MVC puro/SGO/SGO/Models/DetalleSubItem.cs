using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGO.Models
{
    public class DetalleSubItem
    {
        public int ID { get; set; }
        public virtual SubItemDeItem SubItemDeItem { get; set; }
        public virtual Obra Obra { get; set; }
        public double Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
    }
}
