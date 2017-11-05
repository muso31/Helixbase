using Sitecore.Mvc.Controllers;
using System.Web.Mvc;
using Helixbase.Feature.Hero.Mediators;
using Helixbase.Foundation.Core.Exceptions;

namespace Helixbase.Feature.Hero.Controllers
{
    public class HeroController : SitecoreController
    {
        private readonly IHeroMediator _heroMediator;

        public HeroController(IHeroMediator heroMediator)
        {
            _heroMediator = heroMediator;
        }

        public ActionResult Hero()
        {
            var mediatorResponse = _heroMediator.CreateHeroViewModel();

            switch (mediatorResponse.Code)
            {
                case MediatorCodes.HeroResponse.DataSourceError:
                    return View("~/views/Hero/Error.cshtml");
                case MediatorCodes.HeroResponse.Ok:
                    return View(mediatorResponse.ViewModel);
                default:
                    throw new InvalidMediatorResponseCodeException(mediatorResponse.Code);
            }
        }
    }
}