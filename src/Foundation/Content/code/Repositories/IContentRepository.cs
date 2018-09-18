using System.Collections.Generic;
using Glass.Mapper.Sc;
using Sitecore.Data.Items;

namespace Helixbase.Foundation.Content.Repositories
{
    public interface IContentRepository
    {
        T GetItem<T>(GetItemOptions options) where T : class;
        object GetItem(GetItemOptions options);
        IEnumerable<T> GetItems<T>(GetItemsOptions options) where T : class;
        object GetItems(GetItemsOptions options);
        T GetCurrentItem<T>() where T : class;
        object GetCurrentItem(GetKnownOptions options);
        T GetCurrentItem<T>(GetKnownOptions options) where T : class;
        T GetHomeItem<T>() where T : class;
        T GetHomeItem<T>(GetKnownOptions options) where T : class;
        T GetRootItem<T>() where T : class;
        T GetRootItem<T>(GetKnownOptions options) where T : class;

        T CreateItem<T>(object parentItem, T item) where T : class;
        T CreateItem<T>(CreateByModelOptions options) where T : class;
        T CreateItem<T>(CreateByNameOptions options) where T : class;

        void SaveItem<T>(T item) where T : class;
        void SaveItem(SaveOptions options);

        void MoveItem<T, TP>(T item, TP newParent) where T : class where TP : class;
        void MoveItem(MoveByModelOptions options);

        void DeleteItem<T>(T item) where T : class;
        void DeleteItem(DeleteByModelOptions options);

        Item ContextItem { get; }
    }
}