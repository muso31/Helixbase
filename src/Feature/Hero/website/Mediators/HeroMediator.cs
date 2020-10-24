using Helixbase.Feature.Hero.Factories;
using Helixbase.Feature.Hero.Services;
using Helixbase.Feature.Hero.ViewModels;
using Helixbase.Foundation.Core.Models;
using Helixbase.Foundation.Core.Services;
using Helixbase.Foundation.Core.ViewModels;

namespace Helixbase.Feature.Hero.Mediators
{
    public class HeroMediator : IHeroMediator
    {
        private readonly IHeroService _heroService;
        private readonly IMediatorService _mediatorService;
        private readonly IHeroViewModelFactory _heroViewModelFactory;

        public HeroMediator(IHeroService heroService, IMediatorService mediatorService,
            IHeroViewModelFactory heroViewModelFactory)
        {
            _heroService = heroService;
            _mediatorService = mediatorService;
            _heroViewModelFactory = heroViewModelFactory;
        }

        /// <summary>
        ///     Mediator pattern is overkill in this instance, but here as an example to return response codes to a controller, and
        ///     keep the controller dumb as a result
        /// </summary>
        /// <returns>A mediator response with the result of the view model instantiation</returns>
        public MediatorResponse<HeroViewModel> RequestHeroViewModel()
        {
            var heroItemDataSource = _heroService.GetHeroItems();

            if (heroItemDataSource == null)
            {
                return _mediatorService.GetMediatorResponse<HeroViewModel>(
                    MediatorCodes.HeroResponse.DataSourceError,
                    messageViewModel: new MessageViewModel(_heroService.IsExperienceEditor, Messages.DataSourceMissing, Messages.Header));
            }

            var viewModel =
                _heroViewModelFactory.CreateHeroViewModel(heroItemDataSource, _heroService.IsExperienceEditor);

            if (viewModel == null)
            {
                return _mediatorService.GetMediatorResponse<HeroViewModel>(
                    MediatorCodes.HeroResponse.ViewModelError,
                    messageViewModel: new MessageViewModel(_heroService.IsExperienceEditor, Messages.ViewModelError, Messages.Header));
            }

            return _mediatorService.GetMediatorResponse(MediatorCodes.HeroResponse.Ok, viewModel);
        }
    }
}