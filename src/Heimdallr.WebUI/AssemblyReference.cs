using System.Reflection;

namespace Heimdallr.WebUI;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
