using System.Linq;
using FluentAssertions;
using Helixbase.Foundation.Tools.Reflection;
using NUnit.Framework;

namespace Helixbase.Foundation.ORM.Tests
{
    [TestFixture]
    public class ORMDependencyTests
    {
        [Test]
        public void Foundation_ORMModule_FollowsStableDependency()
        {
            var projectAssemblyReferences =
                GetAssemblies.GetByFilter("Helixbase.Project.*", "Helixbase.Feature.*").Any();

            projectAssemblyReferences.Should().BeFalse();
        }
    }
}
