namespace Heimdallr.WebUI.Services.Configuration;

public class JwtOptions(string audience, string issuer, string signingKey)
{
    public string Audience { get; init; } = audience;

    public string Issuer { get; init; } = issuer;

    public string SigningKey { get; init; } = signingKey;
}
