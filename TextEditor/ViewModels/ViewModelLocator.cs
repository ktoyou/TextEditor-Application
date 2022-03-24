using TextEditor.Services;

namespace TextEditor.ViewModels
{
    internal class ViewModelLocator
    {
        static public ViewModelLocator Instance { get; set; } = new ViewModelLocator();

        public EditorViewModel EditorViewModel { get; set; } = ServiceContainer.GetViewModel<EditorViewModel>();
    }
}
