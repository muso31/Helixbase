using System.Linq;
using FluentAssertions;
using Helixbase.Foundation.Tools.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helixbase.Foundation.Tools.Tests
{
    [TestClass]
    public class ToolsDependencyTests
    {
        /// <summary>
        /// Example using VisualStudio test framework, others use NUnit
        /// </summary>
        [TestMethod]
        public void Foundation_ToolsModule_FollowsStableDependency()
        {
            var projectAssemblyReferences =
                GetAssemblies.GetByFilter("Helixbase.Project.*", "Helixbase.Feature.*").Any();

            projectAssemblyReferences.Should().BeFalse();
        }
    }
}
