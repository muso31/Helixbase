using System.Linq;
using FluentAssertions;
using Helixbase.Foundation.Tools.Reflection;
using NUnit.Framework;

namespace Helixbase.Feature.Hero.Tests.Dependencies
{
    [TestFixture]
    public class HeroDependencyTests
    {
        [Test]
        public void HeroFeature_FollowsStableDependency()
        {
            var projectAssemblyReferences = GetAssemblies
                .GetByFilter("Helixbase.Project.*", "Helixbase.Feature.*")
                .Any(x => !x.FullName.Contains("Helixbase.Feature.Hero"));

            projectAssemblyReferences.Should().BeFalse();

            //Could also use: assembly.Should().NotReference(otherAssembly);
        }
    }
}