using System.Web;
using Glass.Mapper.Sc;
using Helixbase.Feature.Hero.Services;
using Helixbase.Feature.Hero.ViewModels;
using Helixbase.Foundation.Core.Services;
using Helixbase.Foundation.Models.Mediators;

namespace Helixbase.Feature.Hero.Mediator
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