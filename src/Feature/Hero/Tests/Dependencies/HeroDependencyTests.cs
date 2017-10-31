using System.Linq;
using FluentAssertions;
using Helixbase.Foundation.Tools.Reflection;
using NUnit.Framework;

namespace Helixbase.Feature.Hero.Tests.Dependencies
{
    [TestFixture]
    public class HelixDependencyTests
    {
        [Test]
        public void HeroFeature_FollowsStableDependency()
        {
            var projectAssemblyReferences = GetAssemblies.GetByFilter("Helixbase.Project.*").Any();

            projectAssemblyReferences.Should().BeFalse();

            //Could also use: assembly.Should().NotReference(otherAssembly);
        }
    }
}
