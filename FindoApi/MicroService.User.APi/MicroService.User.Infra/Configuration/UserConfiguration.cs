using Microsoft.EntityFrameworkCore;

namespace MicroService.User.Infra.Configuration
{
    public static class UserConfiguration
    {
        public static void AddUserConfiguration(this ModelBuilder modelBuilder)
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
    }
}
