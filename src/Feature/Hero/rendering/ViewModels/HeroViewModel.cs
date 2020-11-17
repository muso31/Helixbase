using Sitecore.AspNet.RenderingEngine.Binding.Attributes;
using Sitecore.LayoutService.Client.Response.Model.Fields;


namespace Helixbase.Feature.Hero.ViewModels
{
    public class HeroViewModel
    {
        public TextField HeroTitle { get; set; }

        [SitecoreComponentField]
        public ContentListField<ImageField> HeroImages { get; set; }

        [SitecoreContextProperty]
        public bool IsEditing { get; set; }
    }
}
