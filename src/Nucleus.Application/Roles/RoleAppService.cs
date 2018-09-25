using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nucleus.Application.Roles.Dto;
using Nucleus.Core.Roles;
using Nucleus.EntityFramework;

namespace Nucleus.Application.Roles
{
    public class RoleAppService : IRoleAppService
    {
        private readonly NucleusDbContext _dbContext;
        private readonly RoleManager<Role> _roleManager;

        public RoleAppService(NucleusDbContext dbContext, RoleManager<Role> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        public async Task AddRoleAsync(RoleDto roleDto)
        {
            var role = new Role
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                NormalizedName = roleDto.Name.Normalize()
            };

            var createRoleResult = await _roleManager.CreateAsync(role);
            if (createRoleResult.Succeeded)
            {
                foreach (var permission in roleDto.Permissions)
                {
                    _dbContext.RolePermissions.Add(new RolePermission
                    {
                        PermissionId = permission.Id,
                        RoleId = role.Id
                    });
                }
            }
        }
    }
}