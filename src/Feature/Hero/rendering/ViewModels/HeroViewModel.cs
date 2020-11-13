using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Sitecore.LayoutService.Client.Response.Model.Properties;

namespace Helixbase.Feature.Hero.ViewModels
{
    public class HeroViewModel
    {
        public HtmlString HeroTitle { get; set; }
        public IEnumerable<Image> HeroImages { get; set; }
        public bool IsExperienceEditor { get; set; }
    }
}
