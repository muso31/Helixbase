using Helixbase.Feature.Hero.Models;
using Helixbase.Foundation.Repository.Models;
using System;

namespace Helixbase.Feature.Hero.Repository
{
    public class HeroRepository : IHeroRepository
    {
        private IContentRepository _contentRepository;

        public HeroRepository(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public IHero GetCurrentHero()
        {
            throw new NotImplementedException();
        }

    }
}