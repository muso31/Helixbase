using Helixbase.Feature.Hero.Models;

namespace Helixbase.Feature.Hero.Repository
{
    public interface IHeroRepository
    {
        IHero GetCurrentHero();
    }
}
