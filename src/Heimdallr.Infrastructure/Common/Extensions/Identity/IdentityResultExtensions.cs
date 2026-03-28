using Microsoft.AspNetCore.Identity;

namespace Heimdallr.Infrastructure.Common.Extensions.Identity;

internal static class IdentityResultExtensions
{
    extension(IdentityResult result)
    {
        public bool Failure => !result.Succeeded;
    }
}
