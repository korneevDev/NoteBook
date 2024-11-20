using NoteBookLib.Data.FileHandler;
using NoteBookLib.Domain.FeatureManager;
using NoteBookLib.Entity.DataModel;
using NoteBookLib.Entity.ObjectWrapper;

namespace NoteBookLib.Presentation
{

    public class TextEditor
    {

        private IDocument? _document;
        private readonly FileManager _fileManager;
        private readonly ClipboardInteractor _clipboardManager;
        private readonly UndoRedoManager _undoRedoManager;
        private readonly FindAndReplaceManager _findAndReplaceManager;
        private readonly AutoSaveInteractor _autoSaveManager;
        private Action _updateTitleCallback;


        public TextEditor(ClipboardInteractor clipboardManager, IPathFormatter pathFormatter)
        {
            IExtensionProvider extensionProvider = new IExtensionProvider.Base();
            _fileManager = new FileManager(extensionProvider, pathFormatter);
            _clipboardManager = clipboardManager;
            _undoRedoManager = new UndoRedoManager();
            _findAndReplaceManager = new FindAndReplaceManager();
            _autoSaveManager = new AutoSaveInteractor(_fileManager);
            _updateTitleCallback = () => { };
        }       

        public void SetOnUpdateTitleCallback(Action updateTitleCallback)
        {
            _updateTitleCallback = updateTitleCallback;
            _fileManager.SetOnUpdateTitleCallback(updateTitleCallback);
        }

        public async Task LoadFile(string filePath, string autoSaveInterval)
        {
            _document = await _fileManager.LoadFile(filePath);
            _autoSaveManager.UpdateTimer(autoSaveInterval, _document);
        }

        public async Task CreateFile(string autosaveInterval)
        {
            _document = await _fileManager.LoadFile("");
            _autoSaveManager.UpdateTimer(autosaveInterval, _document);
        }

        public void ShowFile(ITextBox textBox)
        {
            _document?.Show(textBox);
        }

        public List<string> GetAvailableFileExtensions() => _fileManager.GetAvailableExtensions();

        public void SaveFile(string filePath)
        {
            _fileManager.SaveFile(filePath, _document!);
            _undoRedoManager.Clear();

        }

        public void SaveFile()
        {
            if (_document != null)
            {
                _fileManager.SaveFile(_document);
                _undoRedoManager.Clear();
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

        public List<String> GetBuffer() => _clipboardManager.GetBuffer();


        public void Undo()
        {

            _undoRedoManager.Undo(_document!);
        }

        public void Redo()
        {
            _undoRedoManager.Redo(_document!);
        }

        public bool IsRedoAvailable() =>
            _undoRedoManager.IsRedoAvailable();

        public bool IsUndoAvailable() =>
            _undoRedoManager.IsUndoAvailable();

        public bool IsInsertAvailable() =>
            _clipboardManager.IsInsertAvailable();

        public int FindText(string text) =>
            _findAndReplaceManager.FindText(text, _document!);

        public void ReplaceText(string sourceText, string replaceText) =>
            _findAndReplaceManager.ReplaceText(sourceText, replaceText, _document!);

        public void ReplaceAllText(string sourceText, string replaceText) =>
            _findAndReplaceManager.ReplaceAllText(sourceText, replaceText, _document!);

        public void UpdateAutosaveInterval(string interval)
        {
            if (_document != null)
                _autoSaveManager.UpdateTimer(interval, _document);
        }

        public void PrintContent(IPrinter printer)
        {
            _document?.PrintContent(printer);
        }
        public void SetOldValueToBufferTop(int index)
        {
            _clipboardManager.SetOldValueToBufferTop(index);
        }

        public string GetTextFromBuffer() =>
            _clipboardManager.GetLastValueFromBuffer();

        public string GetExtension() => _document?.GetExtension() ?? ".txt";

        public string GetUniqueTitle() =>
                _document?.IsPathExists() ?? false ?
                _fileManager.FormatRepitedPath(_document.GetFileName()) : 
                _document?.GetFileName() ?? "";
    }
}