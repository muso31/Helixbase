using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Helixbase.Foundation.Search.Repositories
{
    public interface ISearchRepository
    {
        IEnumerable<T> GetIndexItems<T>(string databaseName, string indexName, Expression<Func<T, bool>> predicate) where T : class;
    }
}
