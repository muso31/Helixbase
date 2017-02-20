using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Helixbase.Foundation.Search.Repositories
{
    public interface ISearchRepository
    {
        IEnumerable<T> GetIndexItems<T>(string indexName, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy = null, int? amount = null) where T : SearchResultItem;
    }
}
