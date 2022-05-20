using System.Globalization;
using Nucleus.Domain.AppConstants;
using Nucleus.Domain.Entities.Authorization;

namespace Nucleus.DataAccess.Helpers;

public class DbContextDataBuilderHelper
{
    private readonly NucleusDbContext _dbContext;
    public static string AdminRoleName = "Admin";
    public static string AdminUserName = "admin";
    public static string AdminUserEmail = "admin@mail.com";

    public static string MemberRoleName = "Member";
    public static string MemberUserName = "member";
    public static string MemberUserEmail = "member@mail.com";

    private const string PasswordHashFor123Qwe = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw=="; //123qwe

    public DbContextDataBuilderHelper(NucleusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual void SeedData()
    {
        InitializeAdminRoleWithAllPermissions();
        InitializeAdminUserWithAllPermissions();
        InitializeAdminUserRoles();
        InitializeMemberRole();
        InitializeMemberUser();
        InitializeMemberUserRoles();
    }

    private void InitializeAdminUserWithAllPermissions()
    {
        var adminUser = _dbContext.Users.FirstOrDefault(u => u.UserName == AdminUserName);
        if (adminUser == null)
        {
            adminUser = _dbContext.Users.Add(new User
            {
                UserName = AdminUserName,
                Email = AdminUserEmail,
                EmailConfirmed = true,
                NormalizedUserName = AdminUserName.ToUpper(CultureInfo.GetCultureInfo("en-US")),
                NormalizedEmail = AdminUserEmail.ToUpper(CultureInfo.GetCultureInfo("en-US")),
                AccessFailedCount = 5,
                PasswordHash = PasswordHashFor123Qwe
            }).Entity;
            _dbContext.SaveChanges();
        }

        if (adminUser.UserClaims != null)
        {
            adminUser.UserClaims.Clear();
            _dbContext.SaveChanges();
        }

        var userClaims = AppPermissions.GetAll().Select(permission => new UserClaim {ClaimType = AppClaimTypes.Permission, ClaimValue = permission, UserId = adminUser.Id});
        _dbContext.UserClaims.AddRange(userClaims);
        _dbContext.SaveChanges();
    }

    private void InitializeMemberUser()
    {
        if (_dbContext.Users.Any(u => u.UserName == MemberUserName)) return;

        var memberUser = new User
        {
            UserName = MemberUserName,
            Email = MemberUserEmail,
            EmailConfirmed = true,
            NormalizedUserName = MemberUserName.ToUpper(CultureInfo.GetCultureInfo("en-US")),
            NormalizedEmail = MemberUserEmail.ToUpper(CultureInfo.GetCultureInfo("en-US")),
            AccessFailedCount = 5,
            PasswordHash = PasswordHashFor123Qwe
        };

        _dbContext.Users.Add(memberUser);
        _dbContext.SaveChanges();
    }

    private void InitializeAdminRoleWithAllPermissions()
    {
        var adminRole = _dbContext.Roles.FirstOrDefault(r => r.Name == AdminRoleName);
        if (adminRole == null)
        {
            adminRole = _dbContext.Roles.Add(new Role
            {
                Name = AdminRoleName,
                NormalizedName = AdminRoleName.ToUpper(CultureInfo.GetCultureInfo("en-US")),
                IsSystemDefault = true
            }).Entity;
            _dbContext.SaveChanges();
        }

        if (adminRole.RoleClaims != null)
        {
            adminRole.RoleClaims.Clear();
            _dbContext.SaveChanges();
        }

        var roleClaims = AppPermissions.GetAll().Select(permission => new RoleClaim {ClaimType = AppClaimTypes.Permission, ClaimValue = permission, RoleId = adminRole.Id});
        _dbContext.RoleClaims.AddRange(roleClaims);
        _dbContext.SaveChanges();
    }

    private void InitializeMemberRole()
    {
        if (_dbContext.Roles.Any(r => r.Name == MemberRoleName)) return;

        var memberRole = new Role
        {
            Name = MemberRoleName,
            NormalizedName = MemberRoleName.ToUpper(CultureInfo.GetCultureInfo("en-US")),
            IsSystemDefault = true
        };

        _dbContext.Roles.Add(memberRole);
        _dbContext.SaveChanges();
    }

    private void InitializeAdminUserRoles()
    {
        var adminUser = _dbContext.Users.FirstOrDefault(u => u.UserName == AdminUserName);
        var adminRole = _dbContext.Roles.FirstOrDefault(r => r.Name == AdminRoleName);
        if (_dbContext.UserRoles.Any(ur => ur.UserId == adminUser.Id && ur.RoleId == adminRole.Id)) return;

        var userRole = new UserRole
        {
            Role = adminRole,
            User = adminUser
        };

        _dbContext.UserRoles.Add(userRole);
        _dbContext.SaveChanges();
    }

    private void InitializeMemberUserRoles()
    {
        var memberUser = _dbContext.Users.FirstOrDefault(u => u.UserName == MemberUserName);
        var memberRole = _dbContext.Roles.FirstOrDefault(r => r.Name == MemberRoleName);
        if (_dbContext.UserRoles.Any(ur => ur.UserId == memberUser.Id && ur.RoleId == memberRole.Id)) return;

        var userRole = new UserRole
        {
            Role = memberRole,
            User = memberUser
        };

        _dbContext.UserRoles.Add(userRole);
        _dbContext.SaveChanges();
    }
}