using System;
using System.Web;
using Glass.Mapper.Sc;
using Helixbase.Feature.Redirects.Models;
using Helixbase.Foundation.Content.Repositories;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Pipelines.HttpRequest;

namespace Helixbase.Feature.Redirects.Pipelines
{
    public class RedirectResolver : HttpRequestProcessor
    {
        private readonly IContextRepository _contextRepository;
        private readonly ISitecoreService _sitecoreService;

        public RedirectResolver(IContextRepository contextRepository)
        {
            _contextRepository = contextRepository;
        }

        public override void Process(HttpRequestArgs args)
        {
            // TODO: Update this logic as a domain such as https://sitecore.something will not work with redirects
            if (args.Context.Request.Url.OriginalString.ToLower().Contains("/sitecore") ||
                args.Context.Request.Url.AbsolutePath.Equals("/"))
                return;
            // We don't want to redirect an item that exists in Sitecore
            if (Context.Item == null)
                Perform301Redirect();
        }

        private void Perform301Redirect()
        {
            var redirectFolder = _sitecoreService.GetItem<IRedirectFolder>(
                new Query($"fast:{_contextRepository.GetContextSiteRoot()}/*[@@templateid='{Templates.GlobalFolder.TemplateId.ToString("B").ToUpper()}']/*[@@templateid='{Templates.RedirectFolder.TemplateId.ToString("B").ToUpper()}']")
                , x => x.LazyEnable());

            var path = HttpContext.Current.Request.Url.LocalPath;

            if (redirectFolder == null)
                throw new NullReferenceException(Templates.ErrorMessages.NoRedirectFolder);

            foreach (var redirect in redirectFolder.Children)
            {
                if (string.IsNullOrEmpty(redirect.RequestedUrl))
                    throw new NullReferenceException(Templates.ErrorMessages.NoUrlOnItem);

                if (!(redirect is I301Redirect)) continue;

                if (string.Equals(redirect.RequestedUrl, path, StringComparison.CurrentCultureIgnoreCase))
                {
                    var targetItem = _sitecoreService.GetItem<Item>(redirect.RedirectItem.Id);
                    HttpContext.Current.Response.RedirectPermanent(LinkManager.GetItemUrl(targetItem), true);
                }
            }
        }
    }
}