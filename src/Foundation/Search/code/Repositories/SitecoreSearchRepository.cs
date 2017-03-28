using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
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
        public IEnumerable<T> GetIndexItems<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy = null, int? amount = null) where T : SearchResultItem
        {
            return GetIndexItems<T>(Indexes.Web, predicate, orderBy, amount);
        }

        public IEnumerable<T> GetIndexItems<T>(string indexName, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy = null, int? amount = null) where T : SearchResultItem
        {
            var index = ContentSearchManager.GetIndex(indexName);

            using (var context = index.CreateSearchContext())
            {
                var results = context.GetQueryable<T>()
                    .Where(predicate);

                if (orderBy != null)
                    results = results.OrderBy(orderBy);

                if (amount != null)
                    results = results.Take(amount.Value);

                return results;
            }
        }
    }
}