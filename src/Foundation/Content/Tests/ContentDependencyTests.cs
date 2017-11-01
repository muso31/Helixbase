using System.Linq;
using FluentAssertions;
using Helixbase.Foundation.Tools.Reflection;
using NUnit.Framework;

namespace Helixbase.Foundation.Content.Tests
{
    [TestFixture]
    public class ContentDependencyTests
    {
        [Test]
        public void Foundation_ContentModule_FollowsStableDependency()
        {
            var projectAssemblyReferences = GetAssemblies.GetByFilter("Helixbase.Project.*", "Helixbase.Feature.*").Any();

            projectAssemblyReferences.Should().BeFalse();
        }
    }
}