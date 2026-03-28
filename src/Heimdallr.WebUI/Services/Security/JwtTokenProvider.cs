using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Heimdallr.Application.Common.Time;
using Heimdallr.WebUI.Services.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Heimdallr.WebUI.Services.Security;

internal class JwtTokenProvider(IOptions<JwtOptions> options, IDateTimeProvider dateTimeProvider)
{
    public string GetJwtToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            options.Value.Issuer,
            options.Value.Audience,
            signingCredentials: credentials,
            claims: claims,
            expires: dateTimeProvider.UtcNow.AddHours(12).DateTime
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
