using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project.Application.Common.Interfaces;
using Project.Domain.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Infrastructure.Authentication.TokenGenerator
{
    internal class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;

            //IF I USED IOptionMnitor
            /*
                {
                    _jwtSetting = jwtSetting.CurrentValue;
                    jwtSetting.OnChange(updateoption =>
                    {
                        _jwtSetting = updateoption;
                    });
                }
            */
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signing = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var token = new JwtSecurityToken
                (
                    claims: claims,
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                    signingCredentials: signing
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
