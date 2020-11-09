using System.ComponentModel.DataAnnotations;
using Helixbase.Foundation.Core.Platform.Models;

namespace Helixbase.Foundation.Core.Platform.Services
{
    public interface IMediatorService
    {
        MediatorResponse<T> GetMediatorResponse<T>(string code, T viewModel = default(T),
            ValidationResult validationResult = null, object parameters = null, string message = null);
    }
}
