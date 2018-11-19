namespace Nucleus.Application.Users.Dto
{
    public class IsUserGrantToPermissionInput
    {
        public string UserNameOrEmail { get; set; }

        public string PermissionName { get; set; }
    }
}
