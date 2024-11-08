using System.Windows.Controls;
using System.Windows.Documents;

namespace NoteBookUI.View
{

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

    public class TabItemExtended : OnPropertyChangedHandler
    {

        public TextEditor TabViewModel { get; }

        public RichTextBox RichTextBox { get; }

        public string Title
        {
            get => TabViewModel.UpdateTitle();
        }

        public TabItemExtended(TextEditor tabViewModel)
        {
            TabViewModel = tabViewModel;
            RichTextBox = new RichTextBox();
            tabViewModel.ShowFile(new ExtendedRichTextBox(RichTextBox));
            RichTextBox.TextChanged += RichTextBox_TextChanged;
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextRange textRange = new TextRange(RichTextBox.Document.ContentStart,
                                                    RichTextBox.Document.ContentEnd);
            string currentText = textRange.Text;

            TabViewModel.CommitTextChange(currentText);
            OnPropertyChanged(nameof(Title));
        }
    }
}
