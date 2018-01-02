using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGO.Models.ViewModels
{
    public class ObraInfoViewModel
    {
        public int ObraID { get; set; }
        public String NombreObra { get; set; }

        public bool SelectedEnPesos { get; set; }

        public int SelectedRubroID { get; set; }
        public int SelectedSubRubroID { get; set; }
        public int SelectedItemID { get; set; }
        public int SelectedSubItemID { get; set; }

        public IEnumerable<SelectListItem> Rubros { get; set; }

        public IEnumerable<SelectListItem> Subrubros { get; set; }

        public IEnumerable<SelectListItem> Items { get; set; }

        public IEnumerable<SelectListItem> Subitems { get; set; }

        //[JsonProperty(PropertyName = "entregado")]
        public double Entregado { get; set; }

        //[JsonProperty(PropertyName = "aEntregar")]
        public double AEntregar { get; set; }

        //[JsonProperty(PropertyName = "movimientos")]
        public String Movimientos { get; set; }
    }
}