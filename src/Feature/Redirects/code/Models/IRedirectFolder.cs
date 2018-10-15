using Helixbase.Foundation.ORM.Models;
using System.Collections.Generic;

namespace Helixbase.Feature.Redirects.Models
{
    public interface IRedirectFolder : IGlassBase
    {
        IEnumerable<I301Redirect> Children { get; set; }
    }
}
