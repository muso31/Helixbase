using Glass.Mapper.Sc;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Helixbase.Foundation.Search.Repositories
{
    /// <summary>
    /// Retrieve Sitecore items using an index (Search API)
    /// </summary>
    public class SitecoreSearchRepository : ISearchRepository
    {
        public IEnumerable<T> GetIndexItems<T>(string databaseName, string indexName, Expression<Func<T, bool>> predicate) where T : class
        {
            var index = ContentSearchManager.GetIndex(indexName);
            var sitecoreService = new SitecoreService(databaseName);
            var indexItems = new List<T>();

            using (var context = index.CreateSearchContext())
            {
                var results = context.GetQueryable<T>()
                    .Where(predicate);
                // SEARCH REPOSITORY TO BE COMPLETED
                //foreach (var result in results)
                //{
                //    sitecoreService.Map(result);
                //    indexItems.Add(result);
                //}

                return results;
            }
        }
    }
}