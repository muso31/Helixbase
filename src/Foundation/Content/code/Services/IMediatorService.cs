using Helixbase.Foundation.Models.Mediators;
using System.ComponentModel.DataAnnotations;

namespace Helixbase.Foundation.Content.Services
{
    public interface IMediatorService
    {
        MediatorResponse<T> GetMediatorResponse<T>(string code, T viewModel = default(T),
            ValidationResult validationResult = null, object parameters = null, string message = null);
    }
}
