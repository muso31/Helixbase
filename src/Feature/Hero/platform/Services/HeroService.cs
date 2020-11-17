using System.Linq;
using Glass.Mapper.Sc;
using Helixbase.Feature.Hero.Models;
using Helixbase.Foundation.Content.Repositories;
using Helixbase.Foundation.Logging.Repositories;
using Helixbase.Foundation.Search.Models;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data.Items;

namespace Helixbase.Feature.Hero.Services
{
    public class HeroService : IHeroService
    {
        private readonly IContextRepository _contextRepository;
        private readonly IContentRepository _contentRepository;

        private readonly ILogRepository _logRepository;
        private readonly IRenderingRepository _renderingRepository;

        public HeroService(IContextRepository contextRepository, IContentRepository contentRepository,
            ILogRepository logRepository, IRenderingRepository renderingRepository)
        {
            _contextRepository = contextRepository;
            _contentRepository = contentRepository;
            _logRepository = logRepository;
            _renderingRepository = renderingRepository;
        }

        /// <summary>
        ///     Get an item using the rendering repository
        /// </summary>
        /// <returns>The Hero datasource item from the Content API</returns>
        public IHero GetHeroItems(Item contextItem)
        {
            var dataSource = _contentRepository.GetItem<IHero>(new GetItemByIdOptions(contextItem.ID.Guid));

            //var dataSource = _renderingRepository.GetDataSourceItem<IHero>();

            // Basic example of using the wrapped logger
            if (dataSource == null)
                _logRepository.Warn(Logging.Error.DataSourceError);

            return dataSource;
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
            var contextIndex = _contextRepository.GetSearchIndexContext(_contextRepository.GetCurrentItem<Item>());

            using (var context = contextIndex.CreateSearchContext())
            {
                var result = context.GetQueryable<BaseSearchResultItem>().Where(predicate).First();

                return result;

                // OR we could have populated a Glass model using:
                // injectedSitecoreService.Populate(result);
            }
        }
    }
}
