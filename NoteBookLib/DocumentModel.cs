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

        public string Text();

        public IDocumentChange? CalculateChange(string currentText);

        public void AddText(int startIndex, string newText);
        public void RemoveText(int startIndex, string removedText);

        public int FindSubstringIndexes(string text, int index);

        public void ReplaceText(string text, string newText);

        public void ReplaceText(string text, string newText, int index);
    }

    public interface ITextBox
    {
        public void ShowString(string str);
    }

    public class DocumentModel : IDocument
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

        public DocumentModel(string filePath, string content)
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
            textBox.ShowString(_content);
        }

        public async void Save(IFileHandler fileHandler)
        {
            fileHandler.SaveDocument(_filePath, _content);
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

        public string Text() => _content;

        public IDocumentChange? CalculateChange(string newText)
        {
            string oldText = _content;

            if (oldText == newText)
            {
                return null;
            }

            if (oldText.Length > newText.Length)
            {
                return CalculateRemoveTextChange(oldText, newText);
            }

            return CalculateAddTextChange(oldText,newText);

        }

        private IDocumentChange CalculateRemoveTextChange(string oldText, string newText)
        {
            int startIndex = oldText.Zip(newText, (o, n) => o == n).TakeWhile(equal => equal).Count();

            var removedText = oldText.Substring(startIndex, oldText.Length - startIndex);
            newText = newText.Substring(startIndex, newText.Length - startIndex);

            int commonSuffixLength =
                newText.Reverse().Zip(removedText.Reverse(), (o, n) => o == n).TakeWhile(equal => equal).Count();

            removedText = removedText.Substring(0, removedText.Length-commonSuffixLength);

            return new RemoveTextChange(startIndex, removedText);
        }

        private IDocumentChange CalculateAddTextChange(string oldText, string newText)
        {
            
            int startIndex = oldText.Zip(newText, (o, n) => o == n).TakeWhile(equal => equal).Count();

            var addedText = newText.Substring(startIndex, newText.Length-startIndex);
            oldText = oldText.Substring(startIndex, oldText.Length-startIndex);
            int commonSuffixLength =
                oldText.Reverse().Zip(addedText.Reverse(), (o, n) => o == n).TakeWhile(equal => equal).Count();

            addedText = addedText.Substring(0, addedText.Length-commonSuffixLength);

            return new AddTextChange(startIndex, addedText);
        }

        public void AddText(int startIndex, string newText)
        {
            var prefix = _content.Substring(0, Math.Min(_content.Length, startIndex));
            var suffix = "";
            if (startIndex < _content.Length)
            {
                suffix = _content.Substring(startIndex + 1);
            }
            _content = prefix + newText + suffix;
        }

        public void RemoveText(int startIndex, string removedText)
        {
            if (startIndex >= _content.Length)
            {
                startIndex = startIndex - removedText.Length;
            }

            _content = _content.Remove(startIndex, Math.Min(_content.Length, Math.Min(removedText.Length, _content.Length - startIndex)));
        }

        public int FindSubstringIndexes(string text, int index)
        {
            return _content.IndexOf(text, index, StringComparison.Ordinal);
        }

        public void ReplaceText(string text, string newText)
        {
            _content = _content.Replace(text, newText);
        }

        public void ReplaceText(string text, string newText, int index)
        {
            // Разбиваем строку на три части: до, заменяемый участок, после
            string before = _content.Substring(0, index);
            string after = _content.Substring(index + text.Length);

            // Склеиваем с заменой
            _content = before + newText + after;
        }
    }
}
