using System.Web;
using Glass.Mapper.Sc;
using Helixbase.Feature.Hero.Services;
using Helixbase.Feature.Hero.ViewModels;
using Helixbase.Foundation.Core.Models;
using Helixbase.Foundation.Core.Services;

namespace Helixbase.Feature.Hero.Mediators
{
    public class HeroMediator : IHeroMediator
    {
        private readonly IGlassHtml _glassHtml;
        private readonly IHeroService _heroService;
        private readonly IMediatorService _mediatorService;

        public HeroMediator(IGlassHtml glassHtml, IHeroService heroService, IMediatorService mediatorService)
        {
            _glassHtml = glassHtml;
            _heroService = heroService;
            _mediatorService = mediatorService;
        }

        /// <summary>
        ///     Mediator pattern is overkill in this instance, but here as an example to return response codes to a controller, and
        ///     keep the controller dumb as a result
        /// </summary>
        /// <returns>A mediator response with a populated view model</returns>
        public MediatorResponse<HeroViewModel> CreateHeroViewModel()
        {
            var heroItemDataSource = _heroService.GetHeroItems();

            if (heroItemDataSource == null)
                return _mediatorService.GetMediatorResponse<HeroViewModel>(MediatorCodes.HeroResponse.DataSourceError);

            var viewModel = new HeroViewModel
            {
                HeroImages = heroItemDataSource.HeroImages,
                HeroTitle = new HtmlString(_glassHtml.Editable(heroItemDataSource, i => i.HeroTitle,
                    new {EnclosingTag = "h2"})),
                IsExperienceEditor = _heroService.IsExperienceEditor
            };

            return _mediatorService.GetMediatorResponse(MediatorCodes.HeroResponse.Ok, viewModel);
        }
    }
}