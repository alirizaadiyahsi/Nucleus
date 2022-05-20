using Microsoft.AspNetCore.Identity;
using Nucleus.Domain.Entities.Authorization;

namespace Nucleus.Application.Identity;

public interface IIdentityAppService
{
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<User> FindUserByUserNameOrEmailAsync(string userNameOrEmail);
    Task<User> FindUserByEmailAsync(string email);
    Task<User> FindUserByUserNameAsync(string userName);
    Task<IdentityResult> CreateUserAsync(User user, string password);
    Task<string> GenerateEmailConfirmationTokenAsync(User user);
    Task<IdentityResult> ConfirmEmailAsync(User user, string token);
    Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
    Task<string> GeneratePasswordResetTokenAsync(User user);
    Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
    Task<Role> FindRoleByNameAsync(string name);
    IEnumerable<Role> GetRolesByUserName(string userName);
}