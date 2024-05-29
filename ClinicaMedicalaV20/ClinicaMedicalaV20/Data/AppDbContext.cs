using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ClinicaMedicalaV20.Models;



namespace ClinicaMedicalaV20.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Medic> Medici { get; set; }
        public DbSet<Pacient> Pacienti { get; set; }
        public DbSet<Alerte> Alerte { get; set; }
        public DbSet<Date_medicale> Date_medicale { get; set; }
        public DbSet<Date_senzori> Date_senzori { get; set; }
        public DbSet<Recomandari> Recomandari { get; set; }




    }
}
