using System;

namespace Helixbase.Rendering.Configuration
{
    /// <summary>
    /// Example of a custom bound configuration object. Used in the Startup class to bind configuration options
    /// and configure the Sitecore ASP.NET SDK services.
    /// </summary>
    public class SitecoreOptions
    {
        public static readonly string Key = "Sitecore";

        public Uri InstanceUri { get; set; }
        public string LayoutServicePath { get; set; } = "/sitecore/api/layout/render/jss";
        public string DefaultSiteName { get; set; }
        public string ApiKey { get; set; }
        public Uri RenderingHostUri { get; set; }
        public bool EnableExperienceEditor { get; set; }

        public Uri LayoutServiceUri
        {
            get
            {
                if (InstanceUri == null) return null;

                return new Uri(InstanceUri, LayoutServicePath);
            }
        }
    }
}
