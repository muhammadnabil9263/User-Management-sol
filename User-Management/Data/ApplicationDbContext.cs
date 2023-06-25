using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using User_Management.Models;

namespace User_Management.Data
{
    public class ApplicationDbContext:DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }
        public DbSet<Orgnization> Orgnizations { get; set; }
        public DbSet<LocalUser> localUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<LocalUser>().HasOne(e => e.orgnization).WithMany().HasForeignKey(e => e.OrgnizationId);
        }

    }
}
