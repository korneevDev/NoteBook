using System.Windows.Controls;
using System.Windows.Documents;
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
