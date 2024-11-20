using NoteBookLib.Presentation.ObjectWrapper;
using System.Windows.Controls;

namespace NoteBookUI.View
{
    public class ExtendedTextBox(TextBox textBox) : ITextBox
    {
        private readonly TextBox _box = textBox;

        public void ShowString(string str)
        {
            _box.Text = str;
        }
    }
}
