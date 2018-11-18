namespace Nucleus.Application.Users.Dto
{
    public class IsUserInRoleInput
    {
        public string UserNameOrEmail { get; set; }

        public string RoleName { get; set; }
    }
}
