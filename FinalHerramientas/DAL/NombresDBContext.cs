using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace RecuperatorioMVCCore.DAL
{
    public partial class MateDBContext : DbContext
    {
        public MateDBContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()

               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();

                string connectionString = configuration.GetConnectionString("ConnStr");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public virtual DbSet<Operacion> Operaciones { get; set; }        
    }
}
