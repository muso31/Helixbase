using Helixbase.Foundation.Core.Models;
using Helixbase.Foundation.Core.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Helixbase.Foundation.Core.Services
{
    public interface IMediatorService
    {
        MediatorResponse<T> GetMediatorResponse<T>(
            string code,
            T viewModel = default(T),
            ValidationResult validationResult = null,
            object parameters = null,
            MessageViewModel messageViewModel = null);
    }
}