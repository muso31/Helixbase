using System.Linq;
using FluentAssertions;
using Helixbase.Foundation.Tools.Reflection;
using NUnit.Framework;

namespace Helixbase.Foundation.DI.Tests
{
    [TestFixture]
    public class DIDependencyTests
    {
        [Test]
        public void Foundation_DIModule_FollowsStableDependency()
        {
            var projectAssemblyReferences =
                GetAssemblies.GetByFilter("Helixbase.Project.*", "Helixbase.Feature.*").Any();

            projectAssemblyReferences.Should().BeFalse();
        }
    }
}