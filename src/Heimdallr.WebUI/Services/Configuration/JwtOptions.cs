namespace Heimdallr.WebUI.Services.Configuration;

public class JwtOptions
{
    public string Audience { get; init; }

    public string Issuer { get; init; }

    public string SigningKey { get; init; }
}
