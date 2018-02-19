using System;
using System.Web;
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
        private readonly IContentRepository _contentRepository;
        private readonly IContextRepository _contextRepository;

        public RedirectResolver(IContentRepository contentRepository, IContextRepository contextRepository)
        {
            _contentRepository = contentRepository;
            _contextRepository = contextRepository;
        }

        public override void Process(HttpRequestArgs args)
        {
            if (args.Context.Request.Url.OriginalString.ToLower().Contains("/sitecore") ||
                args.Context.Request.Url.AbsolutePath.Equals("/"))
                return;
            // We don't want to redirect an item that exists in Sitecore
            if (Context.Item == null)
                Perform301Redirect();
        }

        private void Perform301Redirect()
        {
            var redirectFolder = _contentRepository.QuerySingle<IRedirectFolder>(
                $"fast:{_contextRepository.GetContextSiteRoot()}/*[@@templateid='{Foundation.Content.Templates.GlobalFolder.TemplateId.ToString("B").ToUpper()}']/*[@@templateid='{Templates.RedirectFolder.TemplateId.ToString("B").ToUpper()}']",
                false, true);

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
                    var targetItem = _contentRepository.GetContentItem<Item>(redirect.RedirectItem.Id.ToString());
                    HttpContext.Current.Response.RedirectPermanent(LinkManager.GetItemUrl(targetItem), true);
                }
            }
        }
    }
}