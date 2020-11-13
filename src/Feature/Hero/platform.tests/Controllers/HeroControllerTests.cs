using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Helixbase.Feature.Hero.LayoutService;
using Helixbase.Feature.Hero.Mediators;
using Helixbase.Feature.Hero.ResolverModels;
using Helixbase.Feature.Hero.Services;
using Helixbase.Foundation.Core.Exceptions;
using Helixbase.Foundation.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Ploeh.AutoFixture;
using Sitecore.LayoutService.Configuration;
using Sitecore.Mvc.Presentation;
using ErrorMessages = Helixbase.Foundation.Core.Exceptions.Constants;

namespace Helixbase.Feature.Hero.Tests.Controllers
{
    [TestClass]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class HeroControllerTests
    {
        private HeroContentResolver _contentResolver;
        private IHeroMediator _heroMediator;
        private IHeroBuilder _heroBuilder;

        [TestInitialize]
        public void Setup()
        {
            _heroMediator = Substitute.For<IHeroMediator>();
            _heroBuilder = Substitute.For<IHeroBuilder>();
            _contentResolver = new HeroContentResolver(_heroBuilder, _heroMediator);
        }

        [TestMethod]
        public void Hero_Action_GivenDataSourceError_ReturnsErrorView()
        {
            var fixture = new Fixture();
            var createViewModelResponse = fixture.Build<MediatorResponse<HeroResolverModel>>()
                .With(x => x.Code, MediatorCodes.HeroResponse.DataSourceError)
                .Create();

            _heroMediator.RequestHeroViewModel().Returns(createViewModelResponse);

            var result = _contentResolver.ResolveContents(new Rendering(), new DefaultRenderingConfiguration()) as HeroResolverModel;

            result.Should().Be(null);
        }

        [TestMethod]
        public void Hero_Action_Throws_InvalidMediatorResponseCodeException()
        {
            var fixture = new Fixture();
            var createViewModelResponse = fixture.Build<MediatorResponse<HeroResolverModel>>()
                .With(x => x.Code, "Unknown code")
                .Create();

            _heroMediator.RequestHeroViewModel().Returns(createViewModelResponse);

            Action act = () => _contentResolver.ResolveContents(new Rendering(), new DefaultRenderingConfiguration());
            act.ShouldThrow<InvalidMediatorResponseCodeException>().Where(e =>
                e.Message.Equals(
                    $"{ErrorMessages.InvalidMediatorResponse.InvalidCodeReturned}: {createViewModelResponse.Code}"));
        }
    }
}
