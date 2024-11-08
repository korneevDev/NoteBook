using NoteBookUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using TextEditorApp.ViewModels;

namespace NoteBookUI.View
{
    public class TabItemExtended : OnPropertyChangedHandler
    {
        // Свойство для хранения текста или данных документа
        public TabViewModel TabViewModel { get; }

        // RichTextBox для редактирования содержимого
        public RichTextBox RichTextBox { get; }

        public string Title
        {
            get => TabViewModel.UpdateTitle();
        }

        public TabItemExtended(TabViewModel tabViewModel)
        {
            TabViewModel = tabViewModel;
            RichTextBox = new RichTextBox();
            RichTextBox.TextChanged += RichTextBox_TextChanged;
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextRange textRange = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);
            string currentText = textRange.Text;

            TabViewModel.CommitTextChange(currentText);
            OnPropertyChanged(nameof(Title));
        }


    }
}
