using Helixbase.Feature.Hero.Models;
using Helixbase.Foundation.Content.Repositories;
using Helixbase.Foundation.Search.Repositories;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Mvc.Presentation;
using System;
using System.Linq;

namespace Helixbase.Feature.Hero.Service
{
    public class HeroService : IHeroService
    {
        private IContentRepository _contentRepository;
        private ISearchRepository _searchRepository;

        public HeroService(IContentRepository contentRepository, ISearchRepository searchRepository)
        {
            _contentRepository = contentRepository;
            _searchRepository = searchRepository;
        }
        /// <summary>
        /// Get an item using the generic content repository
        /// </summary>
        /// <returns>The Hero datasource item from the Content API</returns>
        public IHero GetHeroImages()
        {
            return _contentRepository.GetContentItem<IHero>(RenderingContext.Curre‌nt.Rendering.DataSou‌rce);
        }
        /// <summary>
        /// Get an item using the generic search repository
        /// This method is not required/in use. It is here as an example of how to use the search repository
        /// which gets items from the search index (you must setup SOLR or Lucene first)
        /// </summary>
        /// <returns>The first item based on the Hero template</returns>
        public HeroSearchResultItem GetHeroImagesSearch()
        {
            // First setup your predicate
            var predicate = PredicateBuilder.True<HeroSearchResultItem>();
            predicate = predicate.And(item => item.TemplateId == new Sitecore.Data.ID(Templates.Hero.TemplateId));
            // Todo: setup take/orderby parameters on search
            return _searchRepository.GetIndexItems("web", "sitecore_web_index", predicate).First();
        }
    }
}