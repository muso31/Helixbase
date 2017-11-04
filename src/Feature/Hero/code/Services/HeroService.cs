using System.Linq;
using Helixbase.Feature.Hero.Models;
using Helixbase.Foundation.Content.Repositories;
using Helixbase.Foundation.Search;
using Helixbase.Foundation.Search.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Utilities;

namespace Helixbase.Feature.Hero.Services
{
    public class HeroService : IHeroService
    {
        private readonly IContentRepository _contentRepository;
        public HeroService(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        /// <summary>
        /// Get an item using the generic content repository
        /// </summary>
        /// <returns>The Hero datasource item from the Content API</returns>
        public IHero GetHeroItems()
        {
            var dataSource = _contentRepository.GetDataSource();
            return _contentRepository.GetContentItem<IHero>(dataSource);
        }

        /// <summary>
        /// **** This method is not required/in use. It is here as an example of how to use the computed search field ****
        /// Get an item from the index (you must setup SOLR or Lucene first)
        /// </summary>
        /// <returns>The first item based on the Hero template</returns>
        public BaseSearchResultItem GetHeroImagesSearch()
        {
            // First setup your predicate
            var predicate = PredicateBuilder.True<BaseSearchResultItem>();
            predicate = predicate.And(item => item.Templates.Contains(Constants.Hero.TemplateId));
            predicate = predicate.And(item => !item.Name.Equals("__Standard Values"));

            var index = ContentSearchManager.GetIndex(Indexes.Web);

            using (var context = index.CreateSearchContext())
            {
                var result = context.GetQueryable<BaseSearchResultItem>().Where(predicate).First();

                return result;
            }
        }
    }
}