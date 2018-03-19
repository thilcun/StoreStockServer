using ConfereEstoque.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Data
{
    public class ConfereEstoqueContext : DbContext
    {
        public ConfereEstoqueContext()
            : base("DefaultConnection")
        {

        }
        public DbSet<Ajuste> AjusteSet { get; set; }
        public DbSet<ItemAjuste> ItemAjusteSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Ajuste>().HasKey<int>(e => e.Codigo);
            modelBuilder.Entity<ItemAjuste>().HasKey<int>(e => e.Codigo);

        }
    }
}
