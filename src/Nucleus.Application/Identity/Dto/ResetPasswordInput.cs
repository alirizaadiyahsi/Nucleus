﻿namespace Nucleus.Application.Identity.Dto;

public class ResetPasswordInput
{
    public string UserNameOrEmail { get; set; }

    public string Password { get; set; }

    public string Token { get; set; }
}