using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Helixbase.Foundation.Search.Repositories
{
    public interface ISearchRepository
    {
        /// <summary>
        /// Searches for items in the default index, "sitecore_web_index", and returns them as the type specified.
        /// </summary>
        /// <typeparam name="T">The model with which to return the items.</typeparam>
        /// <param name="predicate">The predicate of the search.</param>
        /// <param name="orderBy">The order of the search.</param>
        /// <param name="amount">The amount of items to return.</param>
        /// <returns>Return an enumerable of items of the type specified.</returns>
        IEnumerable<T> GetIndexItems<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy = null, int? amount = null) where T : SearchResultItem;

        /// <summary>
        /// Searches for items in the provided index and returns them as the type specified.
        /// </summary>
        /// <typeparam name="T">The model with which to return the items.</typeparam>
        /// <param name="predicate">The predicate of the search.</param>
        /// <param name="orderBy">The order of the search.</param>
        /// <param name="amount">The amount of items to return.</param>
        /// <returns>Return an enumerable of items of the type specified.</returns>
        IEnumerable<T> GetIndexItems<T>(string indexName, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy = null, int? amount = null) where T : SearchResultItem;
    }
}
