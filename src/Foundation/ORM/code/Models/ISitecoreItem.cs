using System;
using System.Collections;
using Sitecore.Globalization;

namespace Helixbase.Foundation.ORM.Models
{
    public interface ISitecoreItem
    {
        Guid Id { get; set; }
        Language Language { get; set; }
        int Version { get; set; }
        IEnumerable BaseTemplateIds { get; set; }
        string TemplateName { get; set; }
        Guid TemplateId { get; set; }
        string Name { get; set; }
    }
}
