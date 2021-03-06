//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SGO.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Obra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Obra()
        {
            this.DetalleSubItem = new HashSet<DetalleSubItem>();
            this.Movimiento = new HashSet<Movimiento>();
        }
    
        public int ID { get; set; }
        public double Coeficiente { get; set; }
        public System.DateTime InsFecha { get; set; }
        public Nullable<int> ModUsuarioID { get; set; }
        public string Nombre { get; set; }
        public bool Finalizada { get; set; }
        public System.DateTime ModFecha { get; set; }
        public Nullable<int> DepartamentoID { get; set; }
        public Nullable<int> ClienteID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleSubItem> DetalleSubItem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Movimiento> Movimiento { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Cliente Cliente1 { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}
