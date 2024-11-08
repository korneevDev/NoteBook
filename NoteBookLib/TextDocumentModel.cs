namespace NoteBookUI
{
    public interface IDocument
    {
        public string Title();
        public bool CanBeRemoved();

        public void SetNewContent(string content);

        public void Show(ITextBox textBox);
    }

    public interface ITextBox
    {
        public void ShowString(string str);
    }
        public class TextDocumentModel : IDocument
    { 
        public string _filePath;
        private string _content;
        private bool _isModified;

        public TextDocumentModel()
        {
            this._filePath = "";
            this._content = "";
            this._isModified = false;
        }

        public TextDocumentModel(string filePath, string content)
        {
            this._filePath = filePath;
            this._content = content;
            this._isModified = false;
        }

        public void SetNewContent(string content)
        {
            this._content = content;
            this._isModified = true;
        }

        public string Title() => (string.IsNullOrEmpty(_filePath) ? "New _document" : Path.GetFileName(_filePath)) + 
            (_isModified ? " *" : "");

        public bool CanBeRemoved() => !_isModified;

        public void Show(ITextBox textBox)
        {
            textBox.ShowString(this._content);
        }
        }
}
