using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nucleus.Application.Permissions.Dto;
using Nucleus.Application.Roles.Dto;
using Nucleus.Core.Roles;
using Nucleus.EntityFramework;
using Nucleus.Utilities.Collections;
using Nucleus.Utilities.Extensions.Collections;
using Nucleus.Utilities.Extensions.PrimitiveTypes;

namespace Nucleus.Application.Roles
{
    public class RoleAppService : IRoleAppService
    {
        private readonly NucleusDbContext _dbContext;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleAppService(NucleusDbContext dbContext, RoleManager<Role> roleManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IPagedList<RoleListOutput>> GetRolesAsync(RoleListInput input)
        {
            var query = _roleManager.Roles.Where(
                    !input.Filter.IsNullOrEmpty(),
                    predicate => predicate.Name.Contains(input.Filter))
                .OrderBy(input.SortBy);

            var rolesCount = await query.CountAsync();
            var roles = query.PagedBy(input.PageIndex, input.PageSize).ToList();
            var roleListOutput = _mapper.Map<List<RoleListOutput>>(roles);

            return roleListOutput.ToPagedList(rolesCount);
        }

        public async Task<GetRoleForCreateOrUpdateOutput> GetRoleForCreateOrUpdateAsync(Guid id)
        {
            var allPermissions = _mapper.Map<List<PermissionDto>>(_dbContext.Permissions).OrderBy(p => p.DisplayName).ToList();
            var getRoleForCreateOrUpdateOutput = new GetRoleForCreateOrUpdateOutput
            {
                AllPermissions = allPermissions
            };

            if (id == Guid.Empty)
            {
                return getRoleForCreateOrUpdateOutput;
            }

            return await GetRoleForCreateOrUpdateOutputAsync(id, allPermissions);
        }

        public async Task<IdentityResult> AddRoleAsync(CreateOrUpdateRoleInput input)
        {
            var role = new Role
            {
                Id = input.Role.Id,
                Name = input.Role.Name
            };

            var createRoleResult = await _roleManager.CreateAsync(role);
            if (createRoleResult.Succeeded)
            {
                GrantPermissionsToRole(input.GrantedPermissionIds, role);
            }

            return createRoleResult;
        }

        public async Task<IdentityResult> EditRoleAsync(CreateOrUpdateRoleInput input)
        {
            var role = await _roleManager.FindByIdAsync(input.Role.Id.ToString());
            if (role.Name == input.Role.Name && role.Id != input.Role.Id)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNameAlreadyExist",
                    Description = "This role name is already exists!"
                });
            }
            role.Name = input.Role.Name;
            role.RolePermissions.Clear();

            var updateRoleResult = await _roleManager.UpdateAsync(role);
            if (updateRoleResult.Succeeded)
            {
                GrantPermissionsToRole(input.GrantedPermissionIds, role);
            }

            return updateRoleResult;
        }

        public async Task<IdentityResult> RemoveRoleAsync(Guid id)
        {
            var role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "RoleNotFound",
                    Description = "Role not found!"
                });
            }

            if (role.IsSystemDefault)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "CannotRemoveSystemRole",
                    Description = "You cannot remove default system roles!"
                });
            }

            var removeRoleResult = await _roleManager.DeleteAsync(role);
            if (!removeRoleResult.Succeeded)
            {
                return removeRoleResult;
            }

            role.RolePermissions.Clear();
            role.UserRoles.Clear();

            return removeRoleResult;
        }

        private void GrantPermissionsToRole(IEnumerable<Guid> grantedPermissionIds, Role role)
        {
            foreach (var permissionId in grantedPermissionIds)
            {
                _dbContext.RolePermissions.Add(new RolePermission
                {
                    PermissionId = permissionId,
                    RoleId = role.Id
                });
            }
        }

        private async Task<GetRoleForCreateOrUpdateOutput> GetRoleForCreateOrUpdateOutputAsync(Guid id, List<PermissionDto> allPermissions)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            var roleDto = _mapper.Map<RoleDto>(role);
            var grantedPermissions = role.RolePermissions.Select(rp => rp.Permission);

            return new GetRoleForCreateOrUpdateOutput
            {
                Role = roleDto,
                AllPermissions = allPermissions,
                GrantedPermissionIds = grantedPermissions.Select(p => p.Id).ToList()
            };
        }
    }
}