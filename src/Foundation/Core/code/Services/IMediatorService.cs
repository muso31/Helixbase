using System.ComponentModel.DataAnnotations;
using Helixbase.Foundation.Models.Mediators;

namespace Helixbase.Foundation.Core.Services
{
    public interface IMediatorService
    {
        MediatorResponse<T> GetMediatorResponse<T>(string code, T viewModel = default(T),
            ValidationResult validationResult = null, object parameters = null, string message = null);
    }
}
