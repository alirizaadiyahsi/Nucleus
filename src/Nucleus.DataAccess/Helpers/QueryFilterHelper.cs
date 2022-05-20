using Microsoft.EntityFrameworkCore;
using Nucleus.DataAccess.Extensions;
using Nucleus.Domain.Entities;

namespace Nucleus.DataAccess.Helpers;

public static class QueryFilterHelper
{
    public static void AddQueryFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                modelBuilder.Entity(entityType.ClrType).AddQueryFilter<ISoftDelete>(e => e.IsDeleted == false);
        }
    }
}