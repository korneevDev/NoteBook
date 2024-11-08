using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Documents;
using TextEditorApp.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace NoteBookUI
{
    public interface IDocument
    {
        string Title();
    }

    public class DocumentModel : OnPropertyChangedHandler
    {
        public string _filePath;
        private string _content;
        private bool _isModified;

        public DocumentModel()
        {
            this._filePath = "";
            this._content = "";
            this._isModified = false;
        }

        public void setNewContent(string content)
        {
            this._content = content;
            this._isModified = true;
        }

        public string Title() => (string.IsNullOrEmpty(_filePath) ? "New Document" : Path.GetFileName(_filePath)) + 
            (_isModified ? " *" : "");

        public bool CanBeRemoved() => !_isModified;

    }
}
