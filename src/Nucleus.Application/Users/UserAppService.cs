using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nucleus.EntityFramework;
using System.Linq.Dynamic.Core;
using Nucleus.Application.Users.Dto;
using Nucleus.Utilities.Collections;
using Nucleus.Utilities.Extensions.Collections;
using Nucleus.Utilities.Extensions.PrimitiveTypes;

namespace Nucleus.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IMapper _mapper;
        private readonly NucleusDbContext _dbContext;

        public UserAppService(
            IMapper mapper,
            NucleusDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IPagedList<UserListOutput>> GetUsersAsync(UserListInput input)
        {
            var query = _dbContext.Users.Where(
                    !input.Filter.IsNullOrEmpty(),
                    predicate => predicate.UserName.ToLowerInvariant().Contains(input.Filter) ||
                                 predicate.Email.Contains(input.Filter))
                .OrderBy(input.Sorting);

            var usersCount = await query.CountAsync();
            var users = query.PagedBy(input.PageIndex, input.PageSize).ToList();
            var userListDtos = _mapper.Map<List<UserListOutput>>(users);

            return userListDtos.ToPagedList(usersCount);
        }
    }
}
