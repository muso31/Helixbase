using Helixbase.Foundation.ORM.Models;

namespace Helixbase.Foundation.Content.Tests.Models
{
    public interface ITestItem : IGlassBase
    {
        string Title { get; set; }
    }
}
