using NoteBookLib.FileHandler;

namespace NoteBookLib.DataModel
{
    public interface IDocument
    {
        public string Title(string defaultValue);
        public bool CanBeRemoved();

        public void PrintContent(IPrinter printer);

        public void SetNewContent(IDocumentContent newContent);

        public void Show(ITextBox textBox);

        public void Save(IFileHandler fileHandler);
        public void Save(string filePath, IFileHandler fileHandler);

        public string GetExtension();

        public bool IsNewFile();

        public IDocumentChange CalculateChange(IDocumentContent newContent);

        public void AddText(int startIndex, string newText);
        public void RemoveText(int startIndex, string removedText);

        public int FindSubstringIndexes(string text, int index);

        public void ReplaceText(string text, string newText);

        public void ReplaceText(string text, string newText, int index);
    }

    public class DocumentModel : IDocument
    {
        private string _filePath;
        private IDocumentContent _content;
        private bool _isModified;

        public DocumentModel()
        {
            _filePath = "";
            _content = new IDocumentContent.TextContent("");
            _isModified = false;
        }

        public DocumentModel(string filePath, IDocumentContent content)
        {
            _filePath = filePath;
            _content = content;
            _isModified = false;
        }

        public void SetNewContent(IDocumentContent content)
        {
            _content = content;
            _isModified = true;
        }

        public string Title(string defaultValue) => (string.IsNullOrEmpty(_filePath) ? defaultValue : Path.GetFileName(_filePath)) +
            (_isModified ? " *" : "");

        public bool CanBeRemoved() => !_isModified;

        public void Show(ITextBox textBox) =>
            _content.ShowContent(textBox);
        

        public async void Save(IFileHandler fileHandler)
        {
            _content.SaveContent(fileHandler, _filePath);
            _isModified = false;
        }

        public async void Save(string filePath, IFileHandler fileHandler)
        {
            _filePath = filePath;
            Save(fileHandler);
            _isModified = false;
        }

        public string GetExtension() => ".txt";

        public bool IsNewFile() => string.IsNullOrEmpty(_filePath);

        public IDocumentChange CalculateChange(IDocumentContent newContent) =>
            _content.CalculateChange(newContent);

        

        public void AddText(int startIndex, string newText) =>
            _content = _content.AddText(startIndex, newText);
        

        public void RemoveText(int startIndex, string removedText) =>
            _content = _content.RemoveText(startIndex, removedText);
        

        public int FindSubstringIndexes(string text, int index) =>
            _content.FindSubstringIndexes(text, index);
        

        public void ReplaceText(string text, string newText) =>
            _content = _content.ReplaceText(text, newText);
        

        public void ReplaceText(string text, string newText, int index) =>
            _content = _content.ReplaceText(text, newText, index);
        

        public void PrintContent(IPrinter printer) =>
            _content.PrintContent(printer);
        
    }
}
