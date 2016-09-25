using System.Linq;
using System.Reflection;

namespace Common.Core.Services
{
    public static class AssemblyHelper
    {
        public static Assembly[] GetReferencedAssemblies()
        {
            return Assembly
                .GetEntryAssembly()
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .ToArray();
        }
    }
}
