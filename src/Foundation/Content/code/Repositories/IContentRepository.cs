using Helixbase.Foundation.Content.Models;
using System;

namespace Helixbase.Foundation.Content.Repositories
{
    public interface IContentRepository
    {
        T GetContentItem<T>(string contentGuid) where T : class;

        T GetCurrentItem<T>() where T : class;
    }
}
