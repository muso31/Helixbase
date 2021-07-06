using System;

namespace Helixbase.Feature.Hero
{
    /// <summary>
    /// https://staticreadonly.com/2019/02/06/structures-vs-static-classes-for-sitecore-template-references/
    /// </summary>
    public static class Constants
    {
        public static class Hero
        {
            public static readonly Guid TemplateId = new Guid("{F3FB3269-FF76-4CA7-8393-6CAF69942E52}");
        }
        public static class HeroContentType
        {
            public static readonly Guid TemplateId = new Guid("{462BB765-F578-4D46-A47B-20D16A1BFD94}");
        }
    }

    public static class Messages
    {
        public const string Header = "Hero";
        public const string DataSourceMissing = "Data source missing";
        public const string ViewModelError = "Could not prepare view model";

    }

    public static class Logging
    {
        public static class Error
        {
            public const string DataSourceError = "The Hero datasource was empty";
        }
    }

    public static class MediatorCodes
    {
        public static class HeroResponse
        {
            public const string DataSourceError = "HeroMediator.CreateHeroViewModel.DataSourceError";
            public const string ViewModelError = "HeroMediator.CreateHeroViewModel.ViewModelError";
            public const string Ok = "HeroMediator.CreateHeroViewModel.Ok";
        }
    }
}