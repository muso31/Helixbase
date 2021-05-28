using Helixbase.Feature.Hero.Mediators;
using Helixbase.Foundation.Core.Exceptions;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

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
            var mediatorResponse = _heroMediator.RequestHeroViewModel();

            switch (mediatorResponse.Code)
            {
                case MediatorCodes.HeroResponse.DataSourceError:
                    return View("~/views/Core/Warning.cshtml", mediatorResponse.MessageViewModel);
                case MediatorCodes.HeroResponse.ViewModelError:
                    return View("~/views/Core/Error.cshtml", mediatorResponse.MessageViewModel);
                case MediatorCodes.HeroResponse.Ok:
                    return View(mediatorResponse.ViewModel);
                default:
                    throw new InvalidMediatorResponseCodeException(mediatorResponse.Code);
            }
        }
    }
}