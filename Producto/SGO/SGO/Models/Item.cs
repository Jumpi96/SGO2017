using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGO.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public virtual Unidad Unidad { get; set; }
        public virtual SubRubro SubRubro { get; set; }
    }
}
