using System.Linq;
using FluentAssertions;
using Helixbase.Foundation.Tools.Reflection;
using NUnit.Framework;

namespace Helixbase.Foundation.Search.Tests
{
    [TestFixture]
    public class SearchDependencyTests
    {
        [Test]
        public void Foundation_SearchModule_FollowsStableDependency()
        {
            var projectAssemblyReferences =
                GetAssemblies.GetByFilter("Helixbase.Project.*", "Helixbase.Feature.*").Any();

            projectAssemblyReferences.Should().BeFalse();
        }
    }
}