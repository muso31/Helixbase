using System;

namespace Helixbase.Feature.Redirects
{
    public class Templates
    {
        public struct GlobalFolder
        {
            public static Guid TemplateId = new Guid("{B2076F9F-E735-4F31-91CF-597524B76A1E}");
        }
        public struct RedirectFolder
        {
            public static Guid TemplateId = new Guid("{06C2205B-CECB-4622-9680-E61E5DF1CB05}");
        }
        public struct RedirectDataItem
        {
            public static Guid TemplateId = new Guid("{C2C7DD8C-2B9A-4675-BEA2-76FAC4DC834E}");
        }
        public struct RedirectContentItem
        {
            public static Guid TemplateId = new Guid("{E85BB7D7-85D1-403E-A178-D0988966583F}");
        }
        public struct ErrorMessages
        {
            public const string NoUrlOnItem = "Could not find a URL value on the redirect item";
            public const string NoRedirectFolder = "Redirect folder not found";
        }
    }
}