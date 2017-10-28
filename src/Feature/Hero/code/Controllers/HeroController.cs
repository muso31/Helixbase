using Sitecore.Mvc.Controllers;
using System.Web.Mvc;
using Helixbase.Feature.Hero.Factories;

namespace Helixbase.Feature.Hero.Controllers
{
    public class HeroController : SitecoreController
    {
        private readonly IHeroFactory _heroFactory;

        public HeroController(IHeroFactory heroFactory)
        {
            _heroFactory = heroFactory;
        }

        public ActionResult Hero()
        {
            var model = _heroFactory.CreateHeroViewModel();
            return View(model);
        }
    }
}