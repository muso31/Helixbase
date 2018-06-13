using System;
using System.Collections.Generic;
using System.Web;
using Glass.Mapper;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Configuration;
using Helixbase.Feature.Redirects.Models;
using Helixbase.Foundation.Content.Repositories;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Pipelines.HttpRequest;
using Context = Sitecore.Context;

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
            var builder = new GetsOptions
            {
                EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase,
                ConstructorParameters = new List<ConstructorParameter>() {  }})

            }

            var redirectFolder = _sitecoreService.GetItems<IRedirectFolder>(x => x.);
            var redirectFolder = _contentRepository.QuerySingle<IRedirectFolder>(
                $"fast:{_contextRepository.GetContextSiteRoot()}/*[@@templateid='{Templates.GlobalFolder.TemplateId.ToString("B").ToUpper()}']/*[@@templateid='{Templates.RedirectFolder.TemplateId.ToString("B").ToUpper()}']",
                false, true);

            var path = HttpContext.Current.Request.Url.LocalPath;

            if (redirectFolder == null)
                throw new NullReferenceException(Templates.ErrorMessages.NoRedirectFolder);

            foreach (var redirect in redirectFolder.Children)
            {
                if (string.IsNullOrEmpty(redirect.RequestedUrl))
                    throw new NullReferenceException(Templates.ErrorMessages.NoUrlOnItem);

                if (!(redirect is I301Redirect)) continue;

                var options = new GetItemOptions()
                {
                    ConstructorParameters = null,
                    TemplateId = 
                }

                if (string.Equals(redirect.RequestedUrl, path, StringComparison.CurrentCultureIgnoreCase))
                {
                    var targetItem = _sitecoreService.GetItem<Item>( redirect.RedirectItem.Id.ToString());
                    HttpContext.Current.Response.RedirectPermanent(LinkManager.GetItemUrl(targetItem), true);
                }
            }
        }
    }
}