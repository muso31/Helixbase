using System.ComponentModel.DataAnnotations;

namespace Helixbase.Foundation.Core.Models
{
    public class MediatorResponse
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public object Parameters { get; set; }

        public ValidationResult ValidationResult { get; set; }
    }

    public class MediatorResponse<T> : MediatorResponse
    {
        public T ViewModel { get; set; }
    }
}
