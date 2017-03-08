using Helixbase.Foundation.Content.Models;
using System.Collections.Generic;

namespace Helixbase.Feature.Redirects.Models
{
    public interface IRedirectFolder : ISitecoreItem
    {
        IEnumerable<I301Redirect> Children { get; set; }
    }
}
