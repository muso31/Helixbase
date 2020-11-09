using System.Collections.Generic;
using Glass.Mapper.Sc;
using Sitecore.Data.Items;

namespace Helixbase.Foundation.Content.Platform.Repositories
{
    public interface IContentRepository
    {
        T GetItem<T>(GetItemOptions options) where T : class;
        object GetItem(GetItemOptions options);
        IEnumerable<T> GetItems<T>(GetItemsOptions options) where T : class;
        object GetItems(GetItemsOptions options);
        T CreateItem<T>(CreateOptions options) where T : class;
        object CreateItem(CreateOptions options);
        void SaveItem(SaveOptions options);
        void MoveItem(MoveByModelOptions options);
        void DeleteItem(DeleteByModelOptions options);

        Item ContextItem { get; }
    }
}
