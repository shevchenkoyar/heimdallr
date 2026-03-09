using System.Security.Cryptography;
using System.Text;
using Heimdallr.Application.Common.Interfaces.Security;

namespace Heimdallr.Infrastructure.Services;

public class Sha256Pbkdf2PasswordHasher : IPasswordHasher
{
    public ValueTask<string> HashAsync(string password, CancellationToken cancellationToken)
    {
        byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);

        byte[] salt = GenerateSalt();
        
        byte[] hashedPassword = Rfc2898DeriveBytes.Pbkdf2(passwordAsBytes, salt, 1000, HashAlgorithmName.SHA256, 64);
        
        return new ValueTask<string>(Convert.ToBase64String(hashedPassword));
    }

    private byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];
        Random.Shared.NextBytes(salt);
        return salt;
    }
}
