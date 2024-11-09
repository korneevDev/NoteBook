namespace NoteBookLib
{
    public interface IDocument
    {
        public string Title(string defaultValue);
        public bool CanBeRemoved();

        public void SetNewContent(string content);

        public void Show(ITextBox textBox);

        public void Save(IFileHandler fileHandler);
        public void Save(string filePath,  IFileHandler fileHandler);

        public string GetExtension();

        public bool IsNewFile();
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

        public string Title(string defaultValue) => (string.IsNullOrEmpty(_filePath) ? defaultValue : Path.GetFileName(_filePath)) + 
            (_isModified ? " *" : "");

        public bool CanBeRemoved() => !_isModified;

        public void Show(ITextBox textBox)
        {
            textBox.ShowString(this._content);
        }

        public void Save(IFileHandler fileHandler)
        {
            fileHandler.SaveDocument(_filePath, _content);
            _isModified = false;
        }

        public void Save(string filePath, IFileHandler fileHandler)
        {
            _filePath = filePath;
            Save(fileHandler);
            _isModified = false;
        }

        public string GetExtension() => ".txt";

        public bool IsNewFile() => string.IsNullOrEmpty(_filePath);
    }
}
