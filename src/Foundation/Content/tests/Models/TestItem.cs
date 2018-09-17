using System;
using System.Collections;
using Sitecore.Globalization;

namespace Helixbase.Foundation.Content.Tests.Models
{
    public class TestItem : ITestItem
    {
        public Guid Id { get; set; }
        public Language Language { get; set; }
        public int Version { get; set; }
        public IEnumerable BaseTemplateIds { get; set; }
        public string TemplateName { get; set; }
        public Guid TemplateId { get; set; }
        public string Name { get; set; }

        public string Title { get; set; }
    }
}
