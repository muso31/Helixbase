using Helixbase.Feature.Hero.Models;
using Helixbase.Feature.Hero.Repository;
using System;

namespace Helixbase.Feature.Hero.Service
{
    public class HeroService : IHeroService
    {
        private IHeroRepository _heroRepository;

        public HeroService(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public IHero GetHeroImages()
        {
            throw new NotImplementedException();
        }
    }
}