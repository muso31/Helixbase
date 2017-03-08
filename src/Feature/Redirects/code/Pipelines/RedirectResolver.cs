using Helixbase.Feature.Redirects.Models;
using Helixbase.Foundation.Content.Repositories;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Pipelines.HttpRequest;
using System.Web;

namespace Helixbase.Feature.Redirects.Pipelines
{
    public class RedirectResolver : HttpRequestProcessor
    {
        private IContentRepository _contentRepository;

        public RedirectResolver(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public override void Process(HttpRequestArgs args)
        {
            if (args.Context.Request.Url.OriginalString.ToLower().Contains("/sitecore"))
                return;
            if (Sitecore.Context.Item == null)
                Perform301Redirect();
        }

        private void Perform301Redirect()
        {
            var redirectSettings = _contentRepository.GetContentItem<IRedirectSettings>(Sitecore.Context.Site.RootPath);
            var path = HttpContext.Current.Request.Url.LocalPath;

            foreach (var redirect in redirectSettings.RedirectFolder.Children)
            {
                if (redirect.RequestedURL?.ToLower() == path.ToLower())
                {
                    var targetItem = _contentRepository.GetContentItem<Item>(redirect.RedirectItem.Id.ToString());
                    HttpContext.Current.Response.RedirectPermanent(LinkManager.GetItemUrl(targetItem), true);
                }
            }
        }
    }
}