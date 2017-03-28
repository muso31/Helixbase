using Helixbase.Foundation.Models.BaseItem;

namespace Helixbase.Feature.Redirects.Models
{
    public interface IRedirectSettings : ISitecoreItem
    {
        IRedirectFolder RedirectFolder { get; set; }
    }
}
