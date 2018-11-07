namespace Nucleus.Application.Users.Dto
{
    public class ChangePasswordInput
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string PasswordRepeat { get; set; }
    }
}
