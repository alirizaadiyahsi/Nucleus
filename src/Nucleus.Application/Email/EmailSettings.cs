namespace Nucleus.Application.Email;

public class EmailSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string SenderUsername { get; set; }
    public string SenderEmail { get; set; }
    public string Password { get; set; }
    public bool EnableSsl { get; set; }
}