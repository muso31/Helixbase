using System;

namespace Helixbase.Feature.Redirects
{
    public class Templates
    {
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
        public struct RedirectSettings
        {
            public static Guid TemplateId = new Guid("{022B5A17-18A6-4266-A226-C2C8845BCD81}");
        }
    }
}