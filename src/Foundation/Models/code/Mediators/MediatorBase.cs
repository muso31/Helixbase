using System.ComponentModel.DataAnnotations;

namespace Helixbase.Foundation.Models.Mediators
{
    //TODO: Refactor this it does not belong in models - also consider DI
    public abstract class MediatorBase
    {
        protected static MediatorResponse<T> GetMediatorResponse<T>(string code, T viewModel = default(T),
            ValidationResult validationResult = null, object parameters = null, string message = null)
        {
            var response = new MediatorResponse<T>
            {
                Code = code,
                ViewModel = viewModel,
                ValidationResult = validationResult,
                Parameters = parameters,
                Message = message
            };

            return response;
        }
    }
}