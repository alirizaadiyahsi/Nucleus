using Microsoft.EntityFrameworkCore;

namespace Nucleus.DataAccess.Extensions;

public static class DbContextExtensions
{
    public static IQueryable<object> Set(this DbContext context, Type t)
    {
        return (IQueryable<object>)context.GetType().GetMethods().FirstOrDefault(x => x.Name == "Set" && x.IsGenericMethod)?.MakeGenericMethod(t).Invoke(context, null);
    }
}