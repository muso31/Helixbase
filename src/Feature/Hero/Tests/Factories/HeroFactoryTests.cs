using FluentAssertions;
using Helixbase.Feature.Hero.Factories;
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
        private HeroFactory _heroFactory;

        [SetUp]
        public void Setup()
        {
            _contentRepository = Substitute.For<IContentRepository>();
            _heroFactory = new HeroFactory(_contentRepository);
        }

        [Test]
        public void GetHeroImages_ReturnsHeroViewModel()
        {
            var fixture = new Fixture();
            var mockViewModel = fixture.Create<HeroViewModel>();

            //_heroFactory.CreateHeroViewModel().Returns(mockViewModel);

            var result = _heroFactory.CreateHeroViewModel();

            result.Should().NotBeNull();
            result.Should().BeOfType<HeroViewModel>();
            //result.Should().Be(mockViewModel);
        }
    }
}
