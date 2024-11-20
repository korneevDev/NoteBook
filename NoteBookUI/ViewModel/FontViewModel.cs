using NoteBookLib;
using NoteBookUI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NoteBookUI.ViewModel
{
    public class FontViewModel
    {
        private readonly TabsViewModel _tabsViewModel;

        private FontFamily _selectedFont;
        private double _selectedFontSize;

        private readonly ObservableCollection<FontFamily> _fonts;
        private readonly ObservableCollection<double> _fontSizes;

        public FontViewModel(TabsViewModel tabsViewModel)
        {
            _tabsViewModel = tabsViewModel;

            // Инициализация доступных шрифтов и размеров
            _fonts = new ObservableCollection<FontFamily>(Fonts.SystemFontFamilies);
            _fontSizes = new ObservableCollection<double>(new[] { 8.0, 10.0, 12.0, 14.0, 16.0, 18.0, 20.0, 24.0, 30.0, 34.0, 40.0, 44.0, 50.0, 52.0, 62.0, 66.0, 68.0, 72.0 });
            _selectedFont = _fonts.FirstOrDefault()!; // Устанавливаем начальный шрифт
            _selectedFontSize = _fontSizes[6]; // Устанавливаем начальный размер

        }

        public FileView CreateNewTab(TextEditor textEditor) => 
            new(textEditor, _selectedFont, _selectedFontSize);

        public PrintWindow CreatePrintWindow(IDocument document) =>
            new(document, _selectedFont, _selectedFontSize);

        public ObservableCollection<FontFamily> GetAvailableFonts() => _fonts;

        public ObservableCollection<double> GetAvailableFontSizes() => _fontSizes;

        public FontFamily GetSelectedFont() => _selectedFont;

        public double GetSelectedFontSize() => _selectedFontSize;

        public void SetSelectedFont(FontFamily selectedFont)
        {
            _selectedFont = selectedFont;
            _tabsViewModel.SetSelectedFont(_selectedFont);
        }

        public void SetSelectedFontSize(double selectedFontSize)
        {
            _selectedFontSize = selectedFontSize;
            _tabsViewModel.SetSelectedFontSize(_selectedFontSize);
        }

    }
}
