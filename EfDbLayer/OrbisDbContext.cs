using Microsoft.EntityFrameworkCore;
using MVCappCoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCappCoreWeb.EfDbLayer
{
    public class OrbisDbContext : DbContext
    {
        public OrbisDbContext(DbContextOptions<OrbisDbContext> options)
                                    : base(options)
        {
        }

        public DbSet<DokumentIZ> DokumentIZ { get; set; }
        public DbSet<Ugovori> Ugovori { get; set; }
        public DbSet<StavkeDokumentaIZ> StavkeDokumentaIZ { get; set; }
        public DbSet<Serials> Serial { get; set; }
      //  public DbSet<Klijent> Klijent { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DokumentIZ>().ToTable("DokumentIZ");
            modelBuilder.Entity<Serials>().ToTable("Serial");
            modelBuilder.Entity<StavkeDokumentaIZ>().ToTable("StavkeDokumentaIZ");
            modelBuilder.Entity<Ugovori>().ToTable("Ugovori");
           // modelBuilder.Entity<Klijent>().ToTable("Klijenti");
        }
    }
}
