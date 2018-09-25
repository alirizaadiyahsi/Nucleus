using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Nucleus.EntityFramework;

namespace Nucleus.Web.Core.ActionFilters
{
    //todo: implement unit of work manager
    public class UnitOfWorkActionFilter : ActionFilterAttribute
    {
        private readonly NucleusDbContext _dbContext;

        public UnitOfWorkActionFilter(NucleusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.HttpContext.Request.Method.Equals("Post", StringComparison.OrdinalIgnoreCase) ||
                context.Exception != null ||
                !context.ModelState.IsValid)
            {
                return;
            }

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
