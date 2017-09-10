using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGO.Models;

namespace SGO.Models
{
    public class SGOContext : DbContext
    {
        public SGOContext (DbContextOptions<SGOContext> options)
            : base(options)
        {
        }

        public DbSet<SGO.Models.Obra> Obra { get; set; }

        public DbSet<SGO.Models.DetalleSubItem> DetalleSubItem { get; set; }

        public DbSet<SGO.Models.Item> Item { get; set; }

        public DbSet<SGO.Models.Movimiento> Movimiento { get; set; }

        public DbSet<SGO.Models.Receptor> Receptor { get; set; }

        public DbSet<SGO.Models.Rubro> Rubro { get; set; }

        public DbSet<SGO.Models.SubItem> SubItem { get; set; }

        public DbSet<SGO.Models.SubRubro> SubRubro { get; set; }

        public DbSet<SGO.Models.TipoItem> TipoItem { get; set; }

        public DbSet<SGO.Models.Unidad> Unidad { get; set; }

        public DbSet<SGO.Models.Usuario> Usuario { get; set; }

        public DbSet<SGO.Models.SubItemDeItem> SubItemDeItem { get; set; }
    }
}
