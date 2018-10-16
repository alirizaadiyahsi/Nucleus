using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nucleus.Application.Permissions.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
using Nucleus.EntityFramework;
using Nucleus.Utilities.Collections;
using Nucleus.Utilities.Extensions.Collections;
using Nucleus.Utilities.Extensions.PrimitiveTypes;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nucleus.Core.Users;

namespace Nucleus.Application.Permissions
{
    public class PermissionAppService : IPermissionAppService
    {
        private readonly NucleusDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public PermissionAppService(
            NucleusDbContext dbContext,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IMapper mapper
            )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IPagedList<PermissionListOutput>> GetPermissionsAsync(PermissionListInput input)
        {
            var query = _dbContext.Permissions.Where(
                    !input.Filter.IsNullOrEmpty(),
                    predicate => predicate.Name.Contains(input.Filter) ||
                                 predicate.DisplayName.Contains(input.Filter))
                .OrderBy(input.Sorting);

            var permissionsCount = await query.CountAsync();
            var permissions = query.PagedBy(input.PageIndex, input.PageSize).ToList();
            var permissionListDtos = _mapper.Map<List<PermissionListOutput>>(permissions);

            return permissionListDtos.ToPagedList(permissionsCount);
        }

        public List<PermissionListOutput> GetAllPermissions()
        {
            var permissionListDtos = _mapper.Map<List<PermissionListOutput>>(_dbContext.Permissions);

            return permissionListDtos;
        }

        public async Task<bool> IsPermissionGrantedToUserAsync(string userNameOrEmail, Guid permissionId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u =>
                u.UserName == userNameOrEmail || u.Email == userNameOrEmail);
            if (user == null)
            {
                return false;
            }

            var grantedPermissions = user.UserRoles
                .Select(ur => ur.Role)
                .SelectMany(r => r.RolePermissions)
                .Select(rp => rp.Permission);

            return grantedPermissions.Any(p => p.Id == permissionId);
        }

        public async Task<bool> IsPermissionGrantedToRoleAsync(Role role, Permission permission)
        {
            var existingRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == role.Id);
            if (existingRole == null)
            {
                return false;
            }

            var grantedPermissions = existingRole.RolePermissions
                .Select(rp => rp.Permission);

            return grantedPermissions.Any(p => p.Name == permission.Name);
        }
    }
}
