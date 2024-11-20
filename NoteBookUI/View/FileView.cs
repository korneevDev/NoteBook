﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NoteBookLib;
using NoteBookUI.Utils;

namespace NoteBookUI.View
{

    public class FileView : OnPropertyChangedHandler
    {

        private readonly TextEditor tabTextEditor;

        public TextBox TextBox { get; }

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

        public FileView(TextEditor tabViewModel, FontFamily font, double size)
        {
            tabTextEditor = tabViewModel;
            TextBox = new TextBox
            {
                AcceptsReturn = true,
                AcceptsTab = true,
                TextWrapping = TextWrapping.Wrap,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                FontFamily = font,
                FontSize = size
            };

            tabViewModel.ShowFile(new ExtendedTextBox(TextBox));
            TextBox.TextChanged += TextBoxTextChanged;

            tabViewModel.SetOnUpdateTitleCallback(() =>
            {
                OnPropertyChanged(nameof(Title));
            });
        }

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            string currentText = TextBox.Text;
            tabTextEditor.CommitTextChange(currentText);
        }


        public bool IsNewFile() => tabTextEditor.IsNewFile();

        public bool CanRemoveTab() => tabTextEditor.CanRemove();

        public void Save()
        {
            tabTextEditor.SaveFile();
        }

        public void UpdateTitle() 
        {
            OnPropertyChanged(nameof(Title));
        }

        public void Save(string fileName)
        {
            tabTextEditor.SaveFile(fileName);
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
            TextBox.TextChanged -= TextBoxTextChanged;
            tabTextEditor.ShowFile(new ExtendedTextBox(TextBox));
            TextBox.TextChanged += TextBoxTextChanged;
        }

        public void Redo()
        {
            tabTextEditor.Redo();
            TextBox.TextChanged -= TextBoxTextChanged;
            tabTextEditor.ShowFile(new ExtendedTextBox(TextBox));
            TextBox.TextChanged += TextBoxTextChanged;
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
                SelectText(index, text.Length);
        }

        public void SelectText(int index, int length)
        {
            TextBox.Select(index, length);
            TextBox.Focus();
        }

        public void FindAndReplaceString(string sourceText, string textToReplace)
        {
            tabTextEditor.ReplaceText(sourceText, textToReplace);
            tabTextEditor.ShowFile(new ExtendedTextBox(TextBox));
        }

        public void ReplaceString(string sourceText, string textToReplace)
        {
            tabTextEditor.ReplaceAllText(sourceText, textToReplace);
            tabTextEditor.ShowFile(new ExtendedTextBox(TextBox));
        }

        public string GetOpenFileTemplate()
        {
            var extensionsList = tabTextEditor.GetAvailableFileExtensions();
            var template = "";
            foreach( var extension in extensionsList)
            {
                if (!string.IsNullOrEmpty(StringResourceManager.GetString(extension)))
                    template += template + "|" + StringResourceManager.GetString(extension) + "|*" + extension;
            }

            return template.Substring(1);
        }

        public void UpdateFontSize(double selectedFontSize)
        {
            TextBox.FontSize = selectedFontSize;
        }

        public void UpdateFont(FontFamily selectedFont)
        {
            TextBox.FontFamily = selectedFont;
        }
    }
}