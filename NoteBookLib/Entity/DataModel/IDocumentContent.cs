using NoteBookLib.Data.FileHandler;
using NoteBookLib.Entity.ObjectWrapper;

namespace NoteBookLib.Entity.DataModel
{
    public interface IDocumentContent
    {
        public void ShowContent(ITextBox box);

        public void PrintContent(IPrinter printer);

        public void SaveContent(IFileHandler fileHandler, string filePath);

        public IDocumentChange CalculateChange(IDocumentContent newContent);

        public IDocumentContent AddText(int startIndex, string newText);

        public IDocumentContent RemoveText(int startIndex, string removedText);

        public int FindSubstringIndexes(string text, int index);

        public IDocumentContent ReplaceText(string text, string newText);

        public IDocumentContent ReplaceText(string text, string newText, int index);

        public class TextContent(string text) : IDocumentContent
        {

            private readonly string _text = text;

            public IDocumentChange CalculateChange(IDocumentContent newContent)
            {
                if (newContent is TextContent newTextContent)
                {
                    string oldText = _text;
                    string newText = newTextContent._text;

                    if (oldText == newText)
                    {
                        return new IDocumentChange.EmptyChange();
                    }

                    if (oldText.Length > newText.Length)
                    {
                        return CalculateRemoveTextChange(oldText, newText);
                    }

                    return CalculateAddTextChange(oldText, newText);
                }
                else throw new ArgumentException(newContent.ToString());
            }

            private IDocumentChange CalculateRemoveTextChange(string oldText, string newText)
            {
                int startIndex = oldText.Zip(newText, (o, n) => o == n).TakeWhile(equal => equal).Count();

                var removedText = oldText[startIndex..];
                newText = newText[startIndex..];

                int commonSuffixLength =
                    newText.Reverse().Zip(removedText.Reverse(), (o, n) => o == n).TakeWhile(equal => equal).Count();

                removedText = removedText[..^commonSuffixLength];

                return new IDocumentChange.RemoveTextChange(startIndex, removedText);
            }

            private IDocumentChange CalculateAddTextChange(string oldText, string newText)
            {

                int startIndex = oldText.Zip(newText, (o, n) => o == n).TakeWhile(equal => equal).Count();

                var addedText = newText[startIndex..];
                oldText = oldText[startIndex..];
                int commonSuffixLength =
                    oldText.Reverse().Zip(addedText.Reverse(), (o, n) => o == n).TakeWhile(equal => equal).Count();

                addedText = addedText[..^commonSuffixLength];

                return new IDocumentChange.AddTextChange(startIndex, addedText);
            }

            public IDocumentContent AddText(int startIndex, string newText)
            {
                var prefix = _text[..Math.Min(_text.Length, startIndex)];
                var suffix = "";
                if (startIndex < _text.Length)
                {
                    suffix = _text[(startIndex + 1)..];
                }
                return new TextContent(prefix + newText + suffix);
            }

            public IDocumentContent RemoveText(int startIndex, string removedText)
            {
                if (startIndex >= _text.Length)
                {
                    startIndex -= removedText.Length;
                }

                return new TextContent(_text.Remove(startIndex,
                        Math.Min(
                            _text.Length,
                            Math.Min(removedText.Length, _text.Length - startIndex)
                            )
                        )
                    );
            }

            public int FindSubstringIndexes(string text, int index)
            {
                return _text.IndexOf(text, index, StringComparison.Ordinal);
            }

            public IDocumentContent ReplaceText(string text, string newText) =>
                 new TextContent(_text.Replace(text, newText));


            public IDocumentContent ReplaceText(string text, string newText, int index)
            {
                // Разбиваем строку на три части: до, заменяемый участок, после
                string before = _text[..index];
                string after = _text[(index + text.Length)..];

                // Склеиваем с заменой
                return new TextContent(before + newText + after);
            }

            public void PrintContent(IPrinter printer)
            {
                printer.Print(_text);
            }

            public void SaveContent(IFileHandler fileHandler, string filePath)
            {
                fileHandler.SaveDocument(filePath, _text);
            }

            public void ShowContent(ITextBox box)
            {
                box.ShowString(_text);
            }
        }
    }
}
