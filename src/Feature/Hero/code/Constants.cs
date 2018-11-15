using System;

namespace Helixbase.Feature.Hero
{
    public class Constants
    {
        public struct Hero
        {
            public static Guid TemplateId = new Guid("{462BB765-F578-4D46-A47B-20D16A1BFD94}");
        }
    }

    public class Logging
    {
        public struct Error
        {
            public const string DataSourceError = "The Hero datasource was empty";
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