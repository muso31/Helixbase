using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;

namespace Headlixbase.Foundation.Search.Models
{
    public class BaseSearchResultItem : SearchResultItem
    {
        [IndexField("alltemplates")]
        public IList<Guid> Templates { get; set; }
    }
}
