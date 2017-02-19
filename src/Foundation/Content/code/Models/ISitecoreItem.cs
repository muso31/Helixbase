using Sitecore.Globalization;
using System;
using System.Collections;

namespace Helixbase.Foundation.Content.Models
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
        string Url { get; set; }
    }
}
