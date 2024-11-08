using System.Windows.Controls;
using System.Windows.Documents;
using NoteBookLib;

namespace NoteBookUI.View
{

    public class TabItemExtended : OnPropertyChangedHandler
    {

        private TextEditor _tabTextEditor { get; }

        public RichTextBox RichTextBox { get; }

        public string Title
        {
            get => _tabTextEditor.UpdateTitle();
        }

        public TabItemExtended(TextEditor tabViewModel)
        {
            _tabTextEditor = tabViewModel;
            RichTextBox = new RichTextBox();
            tabViewModel.ShowFile(new ExtendedRichTextBox(RichTextBox));
            RichTextBox.TextChanged += RichTextBox_TextChanged;
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextRange textRange = new TextRange(RichTextBox.Document.ContentStart,
                                                    RichTextBox.Document.ContentEnd);
            string currentText = textRange.Text;

            _tabTextEditor.CommitTextChange(currentText);
            OnPropertyChanged(nameof(Title));
        }

        public bool isNewFile() => _tabTextEditor.IsNewFile();

        public bool CanRemoveTab() => _tabTextEditor.CanRemove();

        public void Save()
        {
            _tabTextEditor.SaveFile();
            OnPropertyChanged(nameof(Title));
        }

        public void Save(string fileName)
        {
            _tabTextEditor.SaveFile(fileName);
            OnPropertyChanged(nameof(Title));
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
