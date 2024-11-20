

using NoteBookLib.DataModel;
using NoteBookLib.FeatureManager;
using NoteBookLib.FileHandler;

namespace NoteBookLib
{
    public interface IFileHandleInteractor
    {
        public Task LoadFile(string filePath);

        public Task CreateFile();

        public void SaveFile();

        public void SaveFile(string filePath);

    }

    public interface IEditTextInteractor
    {

    }

    

    public class TextEditor
    {

        private IDocument? _document;
        private readonly FileManager _fileManager;
        private readonly ClipboardManager _clipboardManager;
        private readonly UndoRedoManager _undoRedoManager;
        private readonly FindAndReplaceManager _findAndReplaceManager;
        private readonly AutoSaveManager _autoSaveManager;
        private Action _updateTitleCallback;


        public TextEditor(ClipboardManager clipboardManager)
        {
            IExtensionProvider extensionProvider = new IExtensionProvider.Base();
            _fileManager = new FileManager(extensionProvider);
            _clipboardManager = clipboardManager;
            _undoRedoManager = new UndoRedoManager();
            _findAndReplaceManager = new FindAndReplaceManager();
            _autoSaveManager = new AutoSaveManager(this);
            _updateTitleCallback = () => { };
        }

        public void SetOnUpdateTitleCallback(Action updateTitleCallback)
        {
            _updateTitleCallback = updateTitleCallback;
        }

        public async Task LoadFile(string filePath, string autoSaveInterval)
        {
            _document = await _fileManager.LoadFile(filePath);
            _autoSaveManager.UpdateInterval(autoSaveInterval);
        }

        public async Task CreateFile(string autosaveInterval)
        {
            _document = await _fileManager.LoadFile("");
            _autoSaveManager.UpdateInterval(autosaveInterval);
        }

        public void ShowFile(ITextBox textBox)
        {
            _document.Show(textBox);
        }

        public List<string> GetAvailableFileExtensions() => _fileManager.GetAvailableExtensions();

        public async void SaveFile(string filePath)
        {
            _fileManager.SaveFile(filePath, _document!);
            _undoRedoManager.Clear();
            _updateTitleCallback.Invoke();
        }

        public async void SaveFile()
        {
            if (_document != null)
            {
                _fileManager.SaveFile(_document);
                _undoRedoManager.Clear();
                _updateTitleCallback.Invoke();
            }
        }

        public bool IsNewFile() => _document!.IsNewFile();

        public string UpdateTitle(string defaultValue) => _document!.Title(defaultValue);

        public void CommitTextChange(IDocumentContent text)
        {
            IDocumentChange change = _document!.CalculateChange(text);
            _undoRedoManager.AddUndo(change);
            _document.SetNewContent(text);
            _findAndReplaceManager.ClearCounter();
            _updateTitleCallback.Invoke();
        }

        public bool CanRemove() => _document?.CanBeRemoved() ?? true;

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

        public bool IsRedoAvailable() =>
            _undoRedoManager.IsRedoAvailable();

        public bool IsUndoAvailable() =>
            _undoRedoManager.IsUndoAvailable();

        public bool IsInsertAvailable() =>
            _clipboardManager.IsInsertAvailable();

        public int FindText(string text) =>
            _findAndReplaceManager.FindText(text, _document);

        public void ReplaceText(string sourceText, string replaceText) =>
            _findAndReplaceManager.ReplaceText(sourceText, replaceText, _document);

        public void ReplaceAllText(string sourceText, string replaceText) =>
            _findAndReplaceManager.ReplaceAllText(sourceText, replaceText, _document);

        public void UpdateAutosaveInterval(string interval) =>
            _autoSaveManager.UpdateInterval(interval);

        public void PrintContent(IPrinter printer)
        {
            _document.PrintContent(printer);
        }
    }
}