namespace NoteBookLib
{
    public class TextEditor
    {

        private IDocument _document;
        private readonly FileManager _fileManager;

        public TextEditor()
        {
            _fileManager = new FileManager();
        }

        public TextEditor(IDocument document)
        {
            _document = document;
            _fileManager = new FileManager();
        }


        public void LoadFile(string filePath)
        {
            _document = _fileManager.LoadFile(filePath);
        }

        public void CreateFile()
        {
            _document = _fileManager.LoadFile("");
        }

        public void ShowFile(ITextBox textBox)
        {
            _document.Show(textBox);
        }

        public void SaveFile(string filePath)
        {
            _fileManager.SaveFile(filePath, _document);
        }

        public void SaveFile()
        {
            _fileManager.SaveFile(_document);
        }

        public bool IsNewFile() => _document.IsNewFile();

        public string UpdateTitle() => _document.Title();

        public void CommitTextChange(string text)
        {
            _document.SetNewContent(text);
        }

        public bool CanRemove() => _document.CanBeRemoved();

    }
}