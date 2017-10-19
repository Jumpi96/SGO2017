using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGO.Models.ViewModels
{
    public class ObraInfoViewModel
    {
        [JsonProperty(PropertyName="rubros")]
        public Dictionary<int,String> rubros { get; set; }

        [JsonProperty(PropertyName = "subrubros")]
        public Dictionary<int, String> subrubros { get; set; }

        [JsonProperty(PropertyName = "items")]
        public Dictionary<int, String> items { get; set; }

        [JsonProperty(PropertyName = "subitems")]
        public Dictionary<int, String> subitems { get; set; }

        [JsonProperty(PropertyName = "unidad")]
        public String unidad { get; set; }

        [JsonProperty(PropertyName = "entregado")]
        public double entregado { get; set; }

        [JsonProperty(PropertyName = "aEntregar")]
        public double aEntregar { get; set; }

        [JsonProperty(PropertyName = "movimientos")]
        public String movimientos { get; set; }
    }
}