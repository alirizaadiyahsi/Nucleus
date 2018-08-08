using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Nucleus.EntityFramework;

namespace Nucleus.Web.Core.ActionFilters
{
    //todo: add test for this attribute and create a test controller to check
    public class UnitOfWorkFilter : ActionFilterAttribute
    {
        private readonly NucleusDbContext _dbContext;

        public UnitOfWorkFilter(NucleusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.HttpContext.Request.Method.Equals("Post", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (context.Exception == null && context.ModelState.IsValid)
            {
                _dbContext.Database.CommitTransaction();
            }
            else
            {
                _dbContext.Database.RollbackTransaction();
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Method.Equals("Post", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            _dbContext.Database.BeginTransaction();
        }
    }
}
