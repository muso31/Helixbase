using FluentAssertions;
using Glass.Mapper.Sc;
using Helixbase.Feature.Hero.Factories;
using Helixbase.Feature.Hero.Services;
using Helixbase.Feature.Hero.ViewModels;
using Helixbase.Foundation.Content.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Ploeh.AutoFixture;

namespace Helixbase.Feature.Hero.Tests.Factories
{
    [TestClass]
    public class HeroFactoryTests
    {
        private IContentRepository _contentRepository;
        private IGlassHtml _glassHtml;
        private IHeroService _heroService;
        private HeroFactory _heroFactory;

        [TestInitialize]
        public void Setup()
        {
            _contentRepository = Substitute.For<IContentRepository>();
            _glassHtml = Substitute.For<IGlassHtml>();
            _heroService = Substitute.For<IHeroService>();

            _heroFactory = new HeroFactory(_contentRepository, _glassHtml, _heroService);
        }

        [TestMethod]
        public void GetHeroImages_ReturnsHeroViewModel()
        {
            var fixture = new Fixture();
            var result = _heroFactory.CreateHeroViewModel();

            result.Should().NotBeNull();
            result.Should().BeOfType<HeroViewModel>();
        }
    }
}
