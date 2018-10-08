using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddRoleAsync(CreateOrEditRoleInput input)
        {
            var role = new Role
            {
                Id = input.Id,
                Name = input.Name,
                NormalizedName = input.Name.Normalize()
            };

            var createRoleResult = await _roleManager.CreateAsync(role);
            if (createRoleResult.Succeeded)
            {
                foreach (var permission in input.Permissions)
                {
                    _dbContext.RolePermissions.Add(new RolePermission
                    {
                        PermissionId = permission.Id,
                        RoleId = role.Id
                    });
                }
            }
        }

        public async Task<IPagedList<RoleListOutput>> GetRolesAsync(RoleListInput input)
        {
            var query = _roleManager.Roles.Where(
                    !input.Filter.IsNullOrEmpty(),
                    predicate => predicate.Name.ToLowerInvariant().Contains(input.Filter))
                .OrderBy(input.Sorting);

            var rolesCount = await query.CountAsync();
            var roles = query.PagedBy(input.PageIndex, input.PageSize).ToList();
            var roleListDtos = _mapper.Map<List<RoleListOutput>>(roles);

            return roleListDtos.ToPagedList(rolesCount);
        }

        public void RemoveRole(Guid id)
        {
            var role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);

            if (role == null)
            {
                return;
            }

            role.RolePermissions.Clear();
            role.UserRoles.Clear();
            _dbContext.Roles.Remove(role);
        }
    }
}