using System.Windows.Input;
using TextEditorApp.ViewModels;

namespace NoteBookUI.ViewModels
{
    public class TabViewModel
    {

        private DocumentModel Document { get; }

        public TabViewModel()
        {
            Document = new DocumentModel();
        }

        public string UpdateTitle() => Document.Title();

        public void CommitTextChange(string text)
        {
            Document.setNewContent(text);
        }

        public bool CanRemove() => Document.CanBeRemoved();
    }
}