using Helixbase.Foundation.ORM.Models;

namespace Helixbase.Foundation.Content.Tests.Models
{
    public interface ITestItem : ISitecoreItem
    {
        string Title { get; set; }
    }
}
