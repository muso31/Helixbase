using System.Linq;
using Helixbase.Feature.Hero.Models;
using Helixbase.Foundation.Content.Repositories;
using Helixbase.Foundation.Search.Models;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data.Items;

namespace Helixbase.Feature.Hero.Services
{
    public class HeroService : IHeroService
    {
        private readonly IContentRepository _contentRepository;
        private readonly IContextRepository _contextRepository;
        private readonly IRenderingRepository _renderingRepository;

        public HeroService(IRenderingRepository renderingRepository, IContentRepository contentRepository,
            IContextRepository contextRepository)
        {
            _renderingRepository = renderingRepository;
            _contentRepository = contentRepository;
            _contextRepository = contextRepository;
        }

        /// <summary>
        ///     Get an item using the generic content repository
        /// </summary>
        /// <returns>The Hero datasource item from the Content API</returns>
        public IHero GetHeroItems()
        {
            var dataSource = _renderingRepository.GetDataSource();
            return _contentRepository.GetContentItem<IHero>(dataSource);
        }

        /// <summary>
        ///     **** This method is not required/in use. It is here as an example of how to use the computed search field ****
        ///     Get an item from the index
        /// </summary>
        /// <returns>The first item based on the Hero template</returns>
        public BaseSearchResultItem GetHeroImagesSearch()
        {
            // First setup your predicate
            var predicate = PredicateBuilder.True<BaseSearchResultItem>();
            predicate = predicate.And(item => item.Templates.Contains(Constants.Hero.TemplateId));
            predicate = predicate.And(item => !item.Name.Equals("__Standard Values"));

            // We could set the index manually using the line below (do not use magic strings, sample only)
            // var index = ContentSearchManager.GetIndex($"Helixbase_{_contextRepository.GetDatabaseContext()}_index");
            // OR we could automate retrieval of the context index:
            var contextIndex = _contextRepository.GetSearchIndexContext(_contentRepository.GetCurrentItem<Item>());

            using (var context = contextIndex.CreateSearchContext())
            {
                var result = context.GetQueryable<BaseSearchResultItem>().Where(predicate).First();

                return result;
            }
        }

        public bool IsExperienceEditor => _contextRepository.IsExperienceEditor;
    }
}