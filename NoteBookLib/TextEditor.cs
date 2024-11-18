﻿

namespace NoteBookLib
{
    public class TextEditor
    {

        private IDocument _document;
        private readonly FileManager _fileManager;
        private readonly ClipboardManager _clipboardManager;
        private readonly UndoRedoManager _undoRedoManager;
        private readonly FindAndReplaceManager _findAndReplaceManager;
        private readonly AutoSaveManager _autoSaveManager;
        private Func<int> _updateTitleCallback;


        public TextEditor(ClipboardManager clipboardManager)
        {
            _fileManager = new FileManager();
            _clipboardManager = clipboardManager;
            _undoRedoManager = new UndoRedoManager();
            _findAndReplaceManager = new FindAndReplaceManager();
            _autoSaveManager = new AutoSaveManager(this);
            _updateTitleCallback = () => 0;
        }

        public void setOnUpdateTitleCallback(Func<int> updateTitleCallback)
        {
            _updateTitleCallback = updateTitleCallback;
        }

        public async Task LoadFile(string filePath)
        {
            _document = await _fileManager.LoadFile(filePath);
            _autoSaveManager.Start();
        }

        public async Task CreateFile()
        {
            _document = await _fileManager.LoadFile("");
            _autoSaveManager.Start();
        }

        public void ShowFile(ITextBox textBox)
        {
            _document.Show(textBox);
        }

        public List<string> GetAvailableFileExtensions() => _fileManager.GetAvailableExtensions();

        public async void SaveFile(string filePath)
        {
            _fileManager.SaveFile(filePath, _document);
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

        public bool IsNewFile() => _document.IsNewFile();

        public string UpdateTitle(string defaultValue) => _document.Title(defaultValue);

        public void CommitTextChange(string text)
        {
            IDocumentChange change = _document.CalculateChange(text);
            _undoRedoManager.AddUndo(change);
            _document.SetNewContent(text);
            _findAndReplaceManager.ClearCounter();
            _updateTitleCallback.Invoke();
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

        public int FindText(string text) =>
            _findAndReplaceManager.FindText(text, _document);

        public void ReplaceText(string sourceText, string replaceText) =>
            _findAndReplaceManager.ReplaceText(sourceText, replaceText, _document);

        public void ReplaceAllText(string sourceText, string replaceText) =>
            _findAndReplaceManager.ReplaceAllText(sourceText, replaceText, _document);

    }
}