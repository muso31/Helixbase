using System.Web;
using Glass.Mapper.Sc;
using Helixbase.Feature.Hero.Services;
using Helixbase.Feature.Hero.ViewModels;
using Helixbase.Foundation.Content.Repositories;

namespace Helixbase.Feature.Hero.Factories
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IContentRepository _contentRepository;
        private readonly IGlassHtml _glassHtml;
        private readonly IHeroService _heroService;

        public HeroFactory(IContentRepository contentRepository, IGlassHtml glassHtml, IHeroService heroService)
        {
            _contentRepository = contentRepository;
            _glassHtml = glassHtml;
            _heroService = heroService;
        }

        public HeroViewModel CreateHeroViewModel()
        {
            var heroItemDataSource = _heroService.GetHeroItems();

            var viewModel = new HeroViewModel()
            {
                HeroImages = heroItemDataSource.HeroImages,
                HeroTitle = new HtmlString(_glassHtml.Editable(heroItemDataSource, i => i.HeroTitle, new { EnclosingTag = "h2" })),
                IsExperienceEditor = _contentRepository.IsExperienceEditor
            };

            return viewModel;
        }
    }
}