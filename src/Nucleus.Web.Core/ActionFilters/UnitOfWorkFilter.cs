using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Nucleus.EntityFramework;

namespace Nucleus.Web.Core.ActionFilters
{
    public class UnitOfWorkActionFilter : ActionFilterAttribute
    {
        private readonly NucleusDbContext _dbContext;

        public UnitOfWorkActionFilter(NucleusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var dataModificationRequestTypes = new[] { "post", "put", "delete" };
            if (!dataModificationRequestTypes.Contains(context.HttpContext.Request.Method, StringComparer.InvariantCultureIgnoreCase) ||
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
