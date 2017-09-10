using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGO.Models
{
    public class Obra
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Cliente { get; set; }
        public double Coeficiente { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime InsFecha { get; set; }
        public virtual Usuario ModUsuario { get; set; }
        public DateTime ModFecha { get; set; }
        public bool Finalizada { get; set; }
    }
}
