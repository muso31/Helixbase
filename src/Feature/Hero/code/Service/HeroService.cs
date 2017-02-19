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

        public IHero GetHeroImages()
        {
            // Get an item using the generic content repository (Sitecore Content API)
             return _contentRepository.GetContentItem<IHero>(RenderingContext.Curre‌nt.Rendering.DataSou‌rce);

            /* The above line is correct for this feature as we need an item from a datasource. However, if we are not returning
            the likes of a datasource item we should always get our items from an index. Once you have SOLR or Lucence setup, you
            should use something similar to the the line below to return an item from the generic search repository. */
            // SEARCH REPO TO BE COMPLETED*************
            // First setup your predicate
            /* var predicate = PredicateBuilder.True<HeroSearchResultItem>();
             predicate = predicate.And(item => item.TemplateId == Guid.Parse(Templates.Hero.TemplateId)); */

            // Get an item using the generic search repository (Sitecore Search API)
            // return _searchRepository.GetIndexItems("web", "sitecore_web_index", predicate).First();
        }
    }
}