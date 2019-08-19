using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MicroService.User.Infra.Data
{
    public class UserContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Model.User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(x => x.Id).HasColumnName("IdOrganizationUnit").HasColumnType("int");
                entity.Property(x => x.FacebookId).HasColumnName("facebookId").HasColumnType("varchar(255)");
                entity.Property(x => x.GoogleId).HasColumnName("googleId").HasColumnType("varchar(255)").IsRequired();
                entity.Property(x => x.Mail).HasColumnName("mail").HasColumnType("varchar(255)");
                entity.Property(x => x.Password).HasColumnName("password").HasColumnType("varchar(255)");

                entity.HasKey(e => e.Id);
            });

        }

        public DbSet<Domain.Model.User> User { get; set; }

    }
}
