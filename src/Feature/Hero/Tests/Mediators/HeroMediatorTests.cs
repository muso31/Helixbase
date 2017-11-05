using FluentAssertions;
using Glass.Mapper.Sc;
using Helixbase.Feature.Hero.Mediator;
using Helixbase.Feature.Hero.Services;
using Helixbase.Feature.Hero.ViewModels;
using Helixbase.Foundation.Models.Mediators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Helixbase.Feature.Hero.Tests.Mediators
{
    [TestClass]
    public class HeroMediatorTests
    {
        private IGlassHtml _glassHtml;
        private IHeroService _heroService;
        private HeroMediator _heroFactory;

        [TestInitialize]
        public void Setup()
        {
            _glassHtml = Substitute.For<IGlassHtml>();
            _heroService = Substitute.For<IHeroService>();

            _heroFactory = new HeroMediator(_glassHtml, _heroService);
        }

        [TestMethod]
        public void GetHeroImages_ReturnsNullResponseCode()
        {
            _heroService.GetHeroItems().ReturnsNull();

            var result = _heroFactory.CreateHeroViewModel();

            result.Code.Should().Match(MediatorCodes.HeroResponse.DataSourceError);
            result.Should().BeOfType<MediatorResponse<HeroViewModel>>();
        }
    }
}
