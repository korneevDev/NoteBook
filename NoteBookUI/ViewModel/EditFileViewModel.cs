using NoteBookUI.View;
using System.Windows;

namespace NoteBookUI.ViewModel
{
    public class EditFileViewModel
    {
        public void Copy(FileView tab)
        {
            tab.Copy(tab.TextBox.SelectedText);
        }

        public void Cut(FileView tab)
        {
            tab.Copy(tab.TextBox.SelectedText);
            tab.TextBox.SelectedText = "";
        }

        public void Insert(FileView tab)
        {
            int caretIndex = tab.TextBox.CaretIndex;
            tab.TextBox.Text = tab.TextBox.Text.Insert(caretIndex, tab.GetTextFromBuffer());
        }

        public void Undo(FileView tab)
        {
            tab.Undo();
        }

        public void Redo(FileView tab)
        {
            tab.Redo();
        }

        public bool IsRedoAvailable(object parameter) =>
            parameter is FileView extended && extended.IsRedoAvailable();

        public bool IsUndoAvailable(object parameter) =>
            parameter is FileView extended && extended.IsUndoAvailable();

        public bool IsInsertAvailable(object parameter) =>
            parameter is FileView extended && extended.IsInsertAvailable();

        public void FindAndReplace(FileView tab)
        {
            var findWindow = new SearchWindow(tab)
            {
                Owner = Application.Current.Windows
                        .OfType<Window>()
                        .FirstOrDefault(w => w.IsActive)
            };

            findWindow.Show();
        }
    }
}
