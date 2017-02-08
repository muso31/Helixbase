using Helixbase.Feature.Hero.Service;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace Helixbase.Feature.Hero.Controllers
{
    public class HeroController  : SitecoreController
    {
        private IHeroService _heroService;

        public HeroController(IHeroService heroService)
        {
            _heroService = heroService;
        }

        public ActionResult Hero()
        {
            var model = _heroService.GetHeroImages();
            return View(model);
        }
    }
}