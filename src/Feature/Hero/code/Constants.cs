using System;

namespace Helixbase.Feature.Hero
{
    public class Constants
    {
        public struct Hero
        {
            public static Guid TemplateId = new Guid("{F3FB3269-FF76-4CA7-8393-6CAF69942E52}");
        }
    }

    public class MediatorCodes
    {
        public struct HeroResponse
        {
            public const string DataSourceError = "HeroMediator.CreateHeroViewModel.DataSourceError";
            public const string Ok = "HeroMediator.CreateHeroViewModel.Ok";
        }
    }
}