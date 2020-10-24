using Helixbase.Foundation.Core.Models;
using Helixbase.Foundation.Core.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Helixbase.Foundation.Core.Services
{
    public class MediatorService : IMediatorService
    {
        public MediatorResponse<T> GetMediatorResponse<T>(
            string code,
            T viewModel = default(T),
            ValidationResult validationResult = null,
            object parameters = null,
            MessageViewModel messageViewModel = null)
        {
            var response = new MediatorResponse<T>
            {
                Code = code,
                ViewModel = viewModel,
                ValidationResult = validationResult,
                Parameters = parameters,
                MessageViewModel = messageViewModel
            };

            return response;
        }
    }
}