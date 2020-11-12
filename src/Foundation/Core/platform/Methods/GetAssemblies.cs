using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Helixbase.Foundation.Core.Methods
{
    public static class GetAssemblies
    {
        public static Assembly[] GetByFilter(params string[] assemblyFilters)
        {
            var assemblyNames = new HashSet<string>(assemblyFilters.Where(filter => !filter.Contains('*')));
            var wildcardNames = assemblyFilters.Where(filter => filter.Contains('*')).ToArray();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(assembly =>
            {
                var nameToMatch = assembly.GetName().Name;
                if (assemblyNames.Contains(nameToMatch)) return true;

                return wildcardNames.Any(wildcard => IsWildcardMatch(nameToMatch, wildcard));
            })
            .ToArray();

            return assemblies;
        }

        /// <summary>
        /// Checks if a string matches a wildcard argument (using regex)
        /// </summary>
        private static bool IsWildcardMatch(string input, string wildcards)
        {
            return Regex.IsMatch(input, "^" + Regex.Escape(wildcards).Replace("\\*", ".*").Replace("\\?", ".") + "$", RegexOptions.IgnoreCase);
        }
    }
}
