using Helixbase.Feature.Hero.Factories;
using Helixbase.Feature.Hero.ResolverModels;
using Sitecore.Data.Items;

namespace Helixbase.Feature.Hero.Services
{
    public class HeroBuilder : IHeroBuilder
    {
        private readonly IHeroService _heroService;
        private readonly IHeroViewModelFactory _heroViewModelFactory;

        public HeroBuilder(IHeroService heroService, IHeroViewModelFactory heroViewModelFactory)
        {
            _heroService = heroService;
            _heroViewModelFactory = heroViewModelFactory;
        }

        public HeroResolverModel GetHeroModel(Item contextItem)
        {
            var heroItemDataSource = _heroService.GetHeroItems(contextItem);

            if (heroItemDataSource == null)
                return null;

            var viewModel =
                _heroViewModelFactory.CreateHeroViewModel(heroItemDataSource);

            return viewModel;

        }
    }
}