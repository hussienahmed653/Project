﻿namespace Project.Infrastructure.Authentication.TokenGenerator
{
    public class JwtSettings
    {
        public const string jwtsettings = "JWT";
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpirationMinutes { get; set; } = 60;
    }
}
