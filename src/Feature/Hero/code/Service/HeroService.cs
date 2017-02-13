using Helixbase.Feature.Hero.Models;
using Helixbase.Foundation.Repository.Models;
using Sitecore.Mvc.Presentation;

namespace Helixbase.Feature.Hero.Service
{
    public class HeroService : IHeroService
    {
        private IContentRepository _contentRepository;

        public HeroService(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public IHero GetHeroImages()
        {
            return _contentRepository.GetContentItem<IHero>(RenderingContext.Curre‌nt.Rendering.DataSou‌rce);
        }
    }
}