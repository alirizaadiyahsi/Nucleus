using AutoMapper;
using Nucleus.Application.Authorization.Roles.Dto;
using Nucleus.Application.Crud;
using Nucleus.DataAccess;
using Nucleus.Domain.AppConstants;
using Nucleus.Domain.Entities.Authorization;

namespace Nucleus.Application.Authorization.Roles;

public class RoleAppService : CrudAppService<Role, RoleOutput, RoleListOutput, CreateRoleInput, UpdateRoleInput>, IRoleAppService
{
    private readonly NucleusDbContext _dbContext;

    public RoleAppService(NucleusDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
        _dbContext = dbContext;
    }

    public override async Task<RoleOutput> GetAsync(Guid id)
    {
        var roleOutput = await base.GetAsync(id);
        roleOutput.AllPermissions = AppPermissions.GetAll();

        return roleOutput;
    }

    public override async Task<RoleOutput> CreateAsync(CreateRoleInput input)
    {
        var roleOutput = await base.CreateAsync(input);

        AddPermissionsToRole(input.SelectedPermissions, roleOutput.Id);
        SetSelectedPermissions(input.SelectedPermissions, roleOutput);

        return roleOutput;
    }

    public override RoleOutput Update(UpdateRoleInput input)
    {
        var roleOutput = base.Update(input);

        _dbContext.RoleClaims.RemoveRange(_dbContext.RoleClaims.Where(x => x.RoleId == input.Id && x.ClaimType == AppClaimTypes.Permission));
        _dbContext.SaveChanges();

        AddPermissionsToRole(input.SelectedPermissions, roleOutput.Id);
        SetSelectedPermissions(input.SelectedPermissions, roleOutput);

        return roleOutput;
    }

    private static void SetSelectedPermissions(IEnumerable<string> selectedPermissions, RoleOutput roleOutput)
    {
        roleOutput.SelectedPermissions = selectedPermissions;
    }

    private void AddPermissionsToRole(IEnumerable<string> selectedPermissions, Guid roleId)
    {
        foreach (var permission in selectedPermissions)
        {
            _dbContext.RoleClaims.Add(new RoleClaim
            {
                RoleId = roleId,
                ClaimType = AppClaimTypes.Permission,
                ClaimValue = permission
            });
        }
    }
}