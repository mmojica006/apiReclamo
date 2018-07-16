using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WS_Reclamo.Models
{
    public partial class dbContext:DbContext
    {
        public dbContext()
            : base("name=dbContext")
        {

        }
        public virtual DbSet<Reclamo>  Reclamo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
       
        }



    }
}