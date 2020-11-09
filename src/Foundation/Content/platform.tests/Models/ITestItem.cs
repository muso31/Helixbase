using Helixbase.Foundation.ORM.Platform.Models;

namespace Helixbase.Foundation.Content.Platform.Tests.Models
{
    public interface ITestItem : IGlassBase
    {
        string Title { get; set; }
    }
}
