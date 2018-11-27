using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nucleus.Application.Permissions.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
using Nucleus.Core.Users;
using Nucleus.EntityFramework;

namespace Nucleus.Application.Permissions
{
    public class PermissionAppService : IPermissionAppService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly NucleusDbContext _dbContext;

        public PermissionAppService(
            UserManager<User> userManager,
            IMapper mapper,
            NucleusDbContext dbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PermissionDto>> GetGrantedPermissionsAsync(string userNameOrEmail)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u =>
                u.UserName == userNameOrEmail || u.Email == userNameOrEmail);

            var grantedPermissions = user?.UserRoles
                .Select(ur => ur.Role)
                .SelectMany(r => r.RolePermissions)
                .Select(rp => rp.Permission);

            return _mapper.Map<IEnumerable<PermissionDto>>(grantedPermissions);
        }

        public async Task<bool> IsUserGrantedToPermissionAsync(string userNameOrEmail, string permissionName)
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

            return grantedPermissions.Any(p => p.Name == permissionName);
        }

        public void InitializePermissions(List<Permission> permissions)
        {
            _dbContext.RolePermissions.RemoveRange(_dbContext.RolePermissions.Where(rp => rp.RoleId == DefaultRoles.Admin.Id));
            _dbContext.SaveChanges();

            _dbContext.Permissions.RemoveRange(_dbContext.Permissions);
            _dbContext.SaveChanges();

            _dbContext.AddRange(permissions);
            GrantAllPermissionsToAdminRole(permissions);
            _dbContext.SaveChanges();
        }

        private void GrantAllPermissionsToAdminRole(List<Permission> permissions)
        {
            foreach (var permission in permissions)
            {
                _dbContext.RolePermissions.Add(new RolePermission
                {
                    PermissionId = permission.Id,
                    RoleId = DefaultRoles.Admin.Id
                });
            }
        }
    }
}
