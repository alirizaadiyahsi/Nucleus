using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Identity;
using Nucleus.Application.Users.Dto;
using Nucleus.Core.Users;
using Nucleus.EntityFramework;
using Nucleus.Utilities.Collections;
using Nucleus.Utilities.Extensions.Collections;
using Nucleus.Utilities.Extensions.PrimitiveTypes;

namespace Nucleus.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly NucleusDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserAppService(
            NucleusDbContext dbContext,
            IMapper mapper, 
            UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IPagedList<UserListOutput>> GetUsersAsync(UserListInput input)
        {
            var query = _userManager.Users.Where(
                    !input.Filter.IsNullOrEmpty(),
                    predicate => predicate.UserName.ToLowerInvariant().Contains(input.Filter) ||
                                 predicate.Email.Contains(input.Filter))
                .OrderBy(input.Sorting);

            var usersCount = await query.CountAsync();
            var users = query.PagedBy(input.PageIndex, input.PageSize).ToList();
            var userListDtos = _mapper.Map<List<UserListOutput>>(users);

            return userListDtos.ToPagedList(usersCount);
        }

        public void RemoveUser(Guid id)
        {
            var user = _userManager.Users.FirstOrDefault(r => r.Id == id);

            if (user == null)
            {
                return;
            }

            user.UserRoles.Clear();
            _dbContext.Users.Remove(user);
        }
    }
}
