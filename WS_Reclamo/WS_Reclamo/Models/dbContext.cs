namespace WS_Reclamo.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbContext : DbContext
    {
        public dbContext()
            : base("name=ctx")
        {
        }

        public virtual DbSet<CL_RelPerDoc> CL_RelPerDoc { get; set; }
        public virtual DbSet<WU_TablaDato> WU_TablaDato { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CL_RelPerDoc>()
                .Property(e => e.IdPersona)
                .HasPrecision(12, 0);

            modelBuilder.Entity<CL_RelPerDoc>()
                .Property(e => e.CodPais)
                .IsUnicode(false);

            modelBuilder.Entity<CL_RelPerDoc>()
                .Property(e => e.TipoDoc)
                .IsUnicode(false);

            modelBuilder.Entity<CL_RelPerDoc>()
                .Property(e => e.NroDocumento)
                .IsUnicode(false);

            modelBuilder.Entity<CL_RelPerDoc>()
                .Property(e => e.TipoPer)
                .IsUnicode(false);

            modelBuilder.Entity<CL_RelPerDoc>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<CL_RelPerDoc>()
                .Property(e => e.Principal)
                .IsUnicode(false);

            modelBuilder.Entity<CL_RelPerDoc>()
                .Property(e => e.TZ_LOCK)
                .HasPrecision(15, 0);

            modelBuilder.Entity<WU_TablaDato>()
                .Property(e => e.Relacion)
                .IsUnicode(false);

            modelBuilder.Entity<WU_TablaDato>()
                .Property(e => e.Valor)
                .IsUnicode(false);

            modelBuilder.Entity<WU_TablaDato>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);
        }
    }
}
