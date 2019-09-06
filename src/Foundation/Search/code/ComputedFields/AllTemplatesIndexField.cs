using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Linq;

namespace Helixbase.Foundation.Search.ComputedFields
{
    public class AllTemplatesIndexField : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var indexItem = indexable as SitecoreIndexableItem;

            if (indexItem == null) return null;

            var item = indexItem.Item;

            var templates = new List<string>();
            GetAllTemplates(item.Template, templates);

            return templates.Distinct().ToList();
        }

        private void GetAllTemplates(TemplateItem baseTemplate, IList<string> templates)
        {
            if (baseTemplate.ID == Sitecore.TemplateIDs.StandardTemplate) return;

            var id = IdHelper.NormalizeGuid(baseTemplate.ID);
            templates.Add(id);

            foreach (var item in baseTemplate.BaseTemplates)
            {
                GetAllTemplates(item, templates);
            }
        }
    }
}