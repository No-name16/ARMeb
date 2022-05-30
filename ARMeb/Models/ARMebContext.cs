using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;


namespace ARMeb.Models
{
    public class ARMebContext : DbContext
    {
        public ARMebContext() : base("DefaultConnection")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Readers>()
                .HasRequired<tblBook>(s => s.TblBooks)
                .WithMany(g => g.Readers)
                .HasForeignKey<int?>(s => s.BookId);
        }
    
    public DbSet<tblUser> Users { get; set; }
        public DbSet<Operations> Listoperations { get; set; }
        public DbSet<tblBook> Books { get; set; }
        public DbSet<Readers> Readers { get; set; }
    }
   
}
