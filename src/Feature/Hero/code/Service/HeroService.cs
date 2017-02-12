using Helixbase.Feature.Hero.Models;
using Helixbase.Foundation.Repository.Models;

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
            return _contentRepository.GetCurrentItem<IHero>();
        }
    }
}