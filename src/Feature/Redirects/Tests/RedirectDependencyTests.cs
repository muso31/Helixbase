using System.Linq;
using FluentAssertions;
using Helixbase.Foundation.Tools.Reflection;
using NUnit.Framework;

namespace Helixbase.Feature.Redirects.Tests
{
    [TestFixture]
    public class RedirectDependencyTests
    {
        [Test]
        public void RedirectFeature_FollowsStableDependency()
        {
            var projectAssemblyReferences = GetAssemblies.GetByFilter("Helixbase.Project.*").Any();

            projectAssemblyReferences.Should().BeFalse();
        }
    }
}
