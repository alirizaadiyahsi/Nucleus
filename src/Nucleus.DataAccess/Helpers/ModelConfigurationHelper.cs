using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nucleus.Domain.Entities.Authorization;

namespace Nucleus.DataAccess.Helpers;

public static class ModelConfigurationHelper
{
    public static void SetModelConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User");
        
        modelBuilder.Entity<Role>().ToTable("Role");
        
        modelBuilder.Entity<UserClaim>().ToTable("UserClaim")
            .HasOne(x => x.User)
            .WithMany(x => x.UserClaims)
            .HasForeignKey(x => x.UserId);
        
        modelBuilder.Entity<UserLogin>().ToTable("UserLogin")
            .HasOne(x => x.User)
            .WithMany(x => x.UserLogins)
            .HasForeignKey(x => x.UserId);
        
        modelBuilder.Entity<UserToken>().ToTable("UserToken")
            .HasOne(x => x.User)
            .WithMany(x => x.UserTokens)
            .HasForeignKey(x => x.UserId);
        
        modelBuilder.Entity<RoleClaim>().ToTable("RoleClaim")
            .HasOne(x => x.Role)
            .WithMany(x => x.RoleClaims)
            .HasForeignKey(x => x.RoleId);

        modelBuilder.Entity((Action<EntityTypeBuilder<UserRole>>)(b =>
        {
            b.ToTable("UserRole");
            b.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            b.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        }));
    }
}