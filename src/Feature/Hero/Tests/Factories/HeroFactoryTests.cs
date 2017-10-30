using FluentAssertions;
using Glass.Mapper.Sc;
using Helixbase.Feature.Hero.Factories;
using Helixbase.Feature.Hero.Services;
using Helixbase.Feature.Hero.ViewModels;
using Helixbase.Foundation.Content.Repositories;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Helixbase.Feature.Hero.Tests.Factories
{
    public class HeroFactoryTests
    {
        private IContentRepository _contentRepository;
        private IGlassHtml _glassHtml;
        private IHeroService _heroService;
        private HeroFactory _heroFactory;

        [SetUp]
        public void Setup()
        {
            _contentRepository = Substitute.For<IContentRepository>();
            _glassHtml = Substitute.For<IGlassHtml>();
            _heroService = Substitute.For<IHeroService>();

            _heroFactory = new HeroFactory(_contentRepository, _glassHtml, _heroService);
        }

        [Test]
        public void GetHeroImages_ReturnsHeroViewModel()
        {
            var fixture = new Fixture();
            var mockViewModel = fixture.Create<HeroViewModel>();

            var result = _heroFactory.CreateHeroViewModel();

            //_heroFactory.CreateHeroViewModel().Returns(mockViewModel);

            result.Should().NotBeNull();
            result.Should().BeOfType<HeroViewModel>();
            //result.Should().Be(mockViewModel);
        }
    }
}
