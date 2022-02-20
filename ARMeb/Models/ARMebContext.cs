using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;


namespace ARMeb.Models
{
    class ARMebContext : DbContext
    {
        public ARMebContext() : base("DefaultConnection")
        {

        }
        public DbSet<tblUser> Users { get; set; }
        public DbSet<Operations> Listoperations { get; set; }
    }
   
}
