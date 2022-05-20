using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Nucleus.DataAccess.Helpers;
using Nucleus.Domain.Entities;
using Nucleus.Domain.Entities.Auditing;
using Nucleus.Domain.Entities.Authorization;

namespace Nucleus.DataAccess;

public class NucleusDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public NucleusDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ModelConfigurationHelper.SetModelConfigurations(modelBuilder);
        QueryFilterHelper.AddQueryFilters(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SetAuditingProperties();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        SetAuditingProperties();
        return base.SaveChanges();
    }

    private void SetAuditingProperties()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    SetCreationAuditedProperties(entry);
                    break;
                case EntityState.Modified:
                    SetModificationAuditedProperties(entry);
                    break;
                case EntityState.Deleted:
                    SetDeletionAuditedProperties(entry);
                    break;
            }
        }
    }

    private void SetDeletionAuditedProperties(EntityEntry entry)
    {
        if (entry.Entity is IHasDeletionTime objectWithDeletionTime)
        {
            objectWithDeletionTime.DeletionTime = DateTime.Now;
        }

        if (entry.Entity is IDeletionAudited objectWithDeleterUser)
        {
            objectWithDeleterUser.DeleterUserId = GetCurrentUserId();
        }

        if (entry.Entity is ISoftDelete objectIsSoftDelete)
        {
            entry.State = EntityState.Modified;
            objectIsSoftDelete.IsDeleted = true;
        }
    }

    private void SetModificationAuditedProperties(EntityEntry entry)
    {
        if (entry.Entity is IHasModificationTime objectWithModificationTime)
        {
            objectWithModificationTime.ModificationTime = DateTime.Now;
        }

        if (entry.Entity is IModificationAudited objectWithModifierUser)
        {
            objectWithModifierUser.ModifierUserId = GetCurrentUserId();
        }
    }

    private void SetCreationAuditedProperties(EntityEntry entry)
    {
        if (entry.Entity is IHasCreationTime objectWithCreationTime)
        {
            objectWithCreationTime.CreationTime = DateTime.Now;
        }

        if (entry.Entity is ICreationAudited objectWithCreatorUser)
        {
            objectWithCreatorUser.CreatorUserId = GetCurrentUserId();
        }
    }

    private Guid? GetCurrentUserId()
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            return null;
        }

        var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue("Id");

        if (!string.IsNullOrEmpty(currentUserId))
        {
            return new Guid(currentUserId);
        }

        return null;
    }
}