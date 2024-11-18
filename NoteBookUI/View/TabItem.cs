using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using NoteBookLib;
using NoteBookUI.Utils;

namespace NoteBookUI.View
{

    public class TabItemExtended : OnPropertyChangedHandler
    {

        private readonly TextEditor tabTextEditor;

        public RichTextBox RichTextBox { get; }

        public string Title
        {
            get => tabTextEditor.UpdateTitle(StringResourceManager.GetString("NewFileTitle"));
        }

        public IDocument Document() => tabTextEditor.Document();

        public string TitleForSaveDialog()
        {
            var title = Title;
            if (title.EndsWith(" *"))
            {
                return title.TrimEnd('*').TrimEnd(' ');
            }

            return title;
        }

        public TabItemExtended(TextEditor tabViewModel)
        {
            tabTextEditor = tabViewModel;
            RichTextBox = new RichTextBox();
            tabViewModel.ShowFile(new ExtendedRichTextBox(RichTextBox));
            RichTextBox.TextChanged += RichTextBox_TextChanged;
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextRange textRange = new TextRange(RichTextBox.Document.ContentStart,
                                                    RichTextBox.Document.ContentEnd);
            string currentText = textRange.Text;

            currentText = currentText.Replace("\r\n", "\n");
            tabTextEditor.CommitTextChange(currentText);
            OnPropertyChanged(nameof(Title));
        }


        public bool IsNewFile() => tabTextEditor.IsNewFile();

        public bool CanRemoveTab() => tabTextEditor.CanRemove();

        public void Save()
        {
            tabTextEditor.SaveFile();
            OnPropertyChanged(nameof(Title));
        }

        public void UpdateTitle() 
        {
            OnPropertyChanged(nameof(Title));
        }

        public void Save(string fileName)
        {
            tabTextEditor.SaveFile(fileName);
            OnPropertyChanged(nameof(Title));
        }

        public void Copy(string text)
        {
            tabTextEditor.CopyText(text);
        }

        public string GetTextFromBuffer() =>
            tabTextEditor.GetTextFromBuffer();
        
        
        public void Undo()
        {
            tabTextEditor.Undo();
            RichTextBox.TextChanged -= RichTextBox_TextChanged;
            tabTextEditor.ShowFile(new ExtendedRichTextBox(RichTextBox));
            RichTextBox.TextChanged += RichTextBox_TextChanged;
        }

        public void Redo()
        {
            tabTextEditor.Redo();
            RichTextBox.TextChanged -= RichTextBox_TextChanged;
            tabTextEditor.ShowFile(new ExtendedRichTextBox(RichTextBox));
            RichTextBox.TextChanged += RichTextBox_TextChanged;
        }

        public bool IsRedoAvailable()
        {
            return tabTextEditor.IsRedoAvailable();
        }

        public bool IsUndoAvailable()
        {
            return tabTextEditor.IsUndoAvailable();
        }

        public void FindNextString(string text)
        {
            int index = tabTextEditor.FindText(text);

            if (index > -1)
                SelectText(RichTextBox, index, index + text.Length);
        }

        public void FindAndReplaceString(string sourceText, string textToReplace)
        {
            tabTextEditor.ReplaceText(sourceText, textToReplace);
            tabTextEditor.ShowFile(new ExtendedRichTextBox(RichTextBox));
        }

        public void ReplaceString(string sourceText, string textToReplace)
        {
            tabTextEditor.ReplaceAllText(sourceText, textToReplace);
            tabTextEditor.ShowFile(new ExtendedRichTextBox(RichTextBox));
        }

        public void SelectText(RichTextBox editor, int startIndex, int endIndex)
        {
            if (editor == null || startIndex < 0 || endIndex < startIndex)
                throw new ArgumentException("Invalid indices or editor.");

            // Получаем весь текст документа, включая символы переноса строк
            string fullText = new TextRange(editor.Document.ContentStart, editor.Document.ContentEnd).Text;


            // Проверяем корректность индексов
            if (startIndex >= fullText.Length || endIndex > fullText.Length)
                throw new ArgumentOutOfRangeException("Indices are out of range.");

            // Находим начальный и конечный TextPointer
            TextPointer startPointer = GetTextPointerAtOffset(editor.Document.ContentStart, fullText, startIndex);
            TextPointer endPointer = GetTextPointerAtOffset(editor.Document.ContentStart, fullText, endIndex);

            if (startPointer != null && endPointer != null)
            {
                // Выделяем текст
                editor.Selection.Select(startPointer, endPointer);
                editor.Focus();
            }
        }

        // Метод для преобразования текстового индекса в TextPointer
        private TextPointer GetTextPointerAtOffset(TextPointer start, string fullText, int offset)
        {
            TextPointer current = start;
            int textOffset = 0;

            // Итерируемся по TextPointer и сравниваем с фактическим текстом
            while (current != null && textOffset < offset)
            {
                if (GetCharacterAtPointer(current) != null)
                {
                    textOffset++;
                }
                current = current.GetPositionAtOffset(1, LogicalDirection.Forward);
            }

            return current;
        }

        // Получение символа в текущей позиции TextPointer
        private char? GetCharacterAtPointer(TextPointer pointer)
        {
            if (pointer == null) return null;

            // Извлекаем текст из текущей позиции
            var range = new TextRange(pointer, pointer.GetPositionAtOffset(1, LogicalDirection.Forward));
            return string.IsNullOrEmpty(range.Text) ? null : range.Text[0];
        }

    }

    public class ExtendedRichTextBox : ITextBox
    {
        private readonly RichTextBox _box;

        public ExtendedRichTextBox(RichTextBox richTextBox)
        {
            _box = richTextBox;
        }

        public void ShowString(string str)
        {
            TextRange textRange = new TextRange(_box.Document.ContentStart, _box.Document.ContentEnd);
            textRange.Text = str;
        }
    }
}
