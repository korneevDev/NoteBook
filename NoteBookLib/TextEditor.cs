
namespace NoteBookLib
{
    public class TextEditor
    {

        private IDocument _document;
        private readonly FileManager _fileManager;
        private readonly ClipboardManager _clipboardManager;
        private readonly UndoRedoManager _undoRedoManager;


        public TextEditor(ClipboardManager clipboardManager)
        {
            _fileManager = new FileManager();
            _clipboardManager = clipboardManager;
            _undoRedoManager = new UndoRedoManager();
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
            _undoRedoManager.Clear();
        }

        public void SaveFile()
        {
            _fileManager.SaveFile(_document);
            _undoRedoManager.Clear();
        }

        public bool IsNewFile() => _document.IsNewFile();

        public string UpdateTitle(string defaultValue) => _document.Title(defaultValue);

        public void CommitTextChange(string text)
        {
            IDocumentChange change = _document.CalculateChange(text);
            _undoRedoManager.AddUndo(change);
            _document.SetNewContent(text);
        }

        public bool CanRemove() => _document.CanBeRemoved();

        public void CopyText(string text)
        {
            _clipboardManager.Copy(text);
        }

        public string GetTextFromBuffer() => _clipboardManager.GetLastValueFromBuffer();

        public IDocument Document() => _document;

        public void Undo()
        {

            _undoRedoManager.Undo(_document);
        }

        public void Redo()
        {
            _undoRedoManager.Redo(_document);
        }

        public bool IsRedoAvailable()
        {
            return _undoRedoManager.IsRedoAvailable();
        }

        public bool IsUndoAvailable()
        {
            return _undoRedoManager.IsUndoAvailable();
        }

    }
}