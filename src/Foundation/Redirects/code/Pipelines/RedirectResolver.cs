using Helixbase.Foundation.Content.Repositories;
using Helixbase.Foundation.Redirects.Models;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Pipelines.HttpRequest;
using System;
using System.Web;

namespace Helixbase.Foundation.Redirects.Pipelines
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
            // TODO: Use GetRootItem (not working, bug in Glass?) and remove Sitecore API call. 
            var redirectSettings = _contentRepository.GetContentItem<IRedirectSettings>(Sitecore.Context.Site.RootPath);

            var path = HttpContext.Current.Request.Url.LocalPath;

            if (redirectSettings.RedirectFolder == null)
                throw new NullReferenceException("No redirect folder found on Site Root item");

            foreach (var redirect in redirectSettings.RedirectFolder.Children)
            {
                // TODO - make Infer Types work with Helix
                //    if (redirect is I301Redirect)
                //    {
                //        var redirect301Item = redirect as I301Redirect;
                //        if (redirect301Item.RequestedURL?.ToLower() == path.ToLower())
                //        {
                //            var targetItem = _contentRepository.GetContentItem<Item>(redirect301Item.RedirectItem.Id.ToString());
                //            HttpContext.Current.Response.RedirectPermanent(LinkManager.GetItemUrl(targetItem), true);
                //        }
                //    }
                if (string.IsNullOrEmpty(redirect.RequestedURL))
                    throw new NullReferenceException("Could not find a URL value on the redirect item");

                if (redirect.RequestedURL.ToLower() == path.ToLower())
                {
                    var targetItem = _contentRepository.GetContentItem<Item>(redirect.RedirectItem.Id.ToString());
                    HttpContext.Current.Response.RedirectPermanent(LinkManager.GetItemUrl(targetItem), true);
                }
            }
        }
    }
}