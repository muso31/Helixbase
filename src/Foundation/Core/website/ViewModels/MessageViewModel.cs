namespace Helixbase.Foundation.Core.ViewModels
{
    public class MessageViewModel
    {
        public MessageViewModel(bool isExperienceEditor, string message, string title = "")
        {
            IsExperienceEditor = isExperienceEditor;
            Message = message;
            Title = title;
        }

        public MessageViewModel() { }

        public bool IsExperienceEditor { get; set; }

        public string Message { get; set; }

        public string Title { get; set; }
    }
}