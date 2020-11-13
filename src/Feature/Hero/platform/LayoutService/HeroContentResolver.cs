using Helixbase.Feature.Hero.Services;
using System.Diagnostics;
using Helixbase.Feature.Hero.Mediators;
using Helixbase.Foundation.Core.Exceptions;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;

namespace Helixbase.Feature.Hero.LayoutService
{
    public class HeroContentResolver : RenderingContentsResolver
    {
        protected readonly IHeroBuilder HeroBuilder;
        private readonly IHeroMediator _heroMediator;

        public HeroContentResolver(IHeroBuilder heroBuilder, IHeroMediator heroMediator)
        {
            Debug.Assert(heroBuilder != null);
            HeroBuilder = heroBuilder;
            _heroMediator = heroMediator;
        }

        public override object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            var mediatorResponse = _heroMediator.RequestHeroViewModel();

            switch (mediatorResponse.Code)
            {
                case MediatorCodes.HeroResponse.DataSourceError:
                case MediatorCodes.HeroResponse.ViewModelError:
                    return null;
                case MediatorCodes.HeroResponse.Ok:
                    return mediatorResponse.ViewModel;
                default:
                    throw new InvalidMediatorResponseCodeException(mediatorResponse.Code);
            }
        }
    }
}