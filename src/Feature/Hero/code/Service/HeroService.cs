using Glass.Mapper.Sc.Web;
using Helixbase.Feature.Hero.Models;
using Helixbase.Foundation.Content.Repositories;
using Helixbase.Foundation.Search.Repositories;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Helixbase.Feature.Hero.Service
{
    public class HeroService : IHeroService
    {
        private IContentRepository _contentRepository;
        private ISearchRepository _searchRepository;
        private IRenderingContext _renderingContext;

        public HeroService(IContentRepository contentRepository, ISearchRepository searchRepository, IRenderingContext renderingContext)
        {
            _contentRepository = contentRepository;
            _searchRepository = searchRepository;
            _renderingContext = renderingContext;
        }
        /// <summary>
        /// Get an item using the generic content repository
        /// </summary>
        /// <returns>The Hero datasource item from the Content API</returns>
        public IHero GetHeroImages()
        {
            // TODO: Wrap Sitecore API call
            return _contentRepository.GetContentItem<IHero>(_renderingContext.GetDataSource());
        }
        /// <summary>
        /// **** This method is not required/in use. It is here as an example of how to use the search repository ****
        /// Get an item from the index using the generic search repository (you must setup SOLR or Lucene first)
        /// </summary>
        /// <returns>The first item based on the Hero template</returns>
        public SearchResultItem GetHeroImagesSearch()
        {
            // First setup your predicate
            var predicate = PredicateBuilder.True<SearchResultItem>();
            predicate = predicate.And(item => item.TemplateId == new Sitecore.Data.ID(Templates.Hero.TemplateId));
            // Order by
            Expression<Func<SearchResultItem, object>> orderBy = item => item.Name;

            return _searchRepository.GetIndexItems("sitecore_web_index", predicate, orderBy).First();
        }
    }
}