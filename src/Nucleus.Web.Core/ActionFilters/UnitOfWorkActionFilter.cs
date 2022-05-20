using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Nucleus.DataAccess;

namespace Nucleus.Web.Core.ActionFilters;

public class UnitOfWorkActionFilter : ActionFilterAttribute
{
    private readonly NucleusDbContext _dbContext;

    public UnitOfWorkActionFilter(NucleusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _dbContext.Database.BeginTransaction();
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null || !context.ModelState.IsValid)
        {
            return;
        }

        try
        {
            _dbContext.Database.CommitTransaction();
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            if (ex is DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                dbUpdateConcurrencyException.Entries.Single().Reload();
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
            }
            else
            {
                _dbContext.Database.RollbackTransaction();
            }
        }
    }
}