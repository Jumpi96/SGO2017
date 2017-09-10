using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGO.Models
{
    public class SubItemDeItem
    {
        public int ID { get; set; }
        public virtual Item Item { get; set; }
        public virtual SubItem SubItem { get; set; }

    }
}
