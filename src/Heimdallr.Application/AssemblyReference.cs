using System.Reflection;

namespace Heimdallr.Application;

internal static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
