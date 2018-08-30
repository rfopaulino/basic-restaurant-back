using Domain.Entity;
using Infrastructure.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure
{
    public class Model : DbContext
    {
        public virtual DbSet<Restaurante> Restaurante { get; set; }
        public virtual DbSet<Prato> Prato { get; set; }

        public Model(DbContextOptions<Model> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RestauranteMap());
            modelBuilder.ApplyConfiguration(new PratoMap());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Model>
    {
        public Model CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var builder = new DbContextOptionsBuilder<Model>();

            var connectionString = configuration.GetConnectionString("DefaultBase");

            builder.UseSqlServer(connectionString);

            return new Model(builder.Options);
        }
    }
}
