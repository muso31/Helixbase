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
            return _contentRepository.GetContentItem<IHero>("{0A275E4A-98DF-4CB3-8A7E-948F53010AE3}");
        }
    }
}