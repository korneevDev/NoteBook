using NoteBookLib.Presentation;
using NoteBookUI.View;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace NoteBookUI.ViewModel
{
    public class FontViewModel
    {
        private readonly TabsViewModel _tabsViewModel;
        private readonly Printer _printer;

        private FontFamily _selectedFont;
        private double _selectedFontSize;

        private readonly ObservableCollection<FontFamily> _fonts;
        private readonly ObservableCollection<double> _fontSizes;

        private SolidColorBrush _selectedTextColor;
        private SolidColorBrush _selectedBackgroundColor;
        private readonly ObservableCollection<SolidColorBrush> _colors;

        public FontViewModel(TabsViewModel tabsViewModel, Printer printer)
        {
            _tabsViewModel = tabsViewModel;
            _printer = printer;

            // Инициализация доступных шрифтов и размеров
            _fonts = new ObservableCollection<FontFamily>(Fonts.SystemFontFamilies);
            _fontSizes = new ObservableCollection<double>(new[] { 8.0, 10.0, 12.0, 14.0, 16.0, 18.0, 20.0, 24.0, 30.0, 34.0, 40.0, 44.0, 50.0, 52.0, 62.0, 66.0, 68.0, 72.0 });
            _selectedFont = _fonts.FirstOrDefault()!; // Устанавливаем начальный шрифт
            _selectedFontSize = _fontSizes[6]; // Устанавливаем начальный размер

            // Инициализация доступных цветов
            _colors = new ObservableCollection<SolidColorBrush>(typeof(Colors).GetProperties()
                .Select(p => new SolidColorBrush((Color)p.GetValue(null)!)));
            _selectedTextColor = new SolidColorBrush(Colors.Black); // Начальный цвет текста
            _selectedBackgroundColor = new SolidColorBrush(Colors.White); // Начальный цвет фона
        }

        public FileView CreateNewTab(TextEditor textEditor) => 
            new(textEditor, _selectedFont, _selectedFontSize, _selectedTextColor, _selectedBackgroundColor);

        public PrintWindow CreatePrintWindow(FileView tab) =>
            new(_printer, tab, _selectedFont, _selectedFontSize, _selectedTextColor, _selectedBackgroundColor);

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

        public ObservableCollection<SolidColorBrush> GetAvailableColors() => _colors;

        public SolidColorBrush GetSelectedTextColor() => _selectedTextColor;

        public void SetSelectedTextColor(SolidColorBrush selectedColor)
        {
            _selectedTextColor = selectedColor;
            _tabsViewModel.SetSelectedTextColor(_selectedTextColor);
        }

        public SolidColorBrush GetSelectedBackgroundColor() => _selectedBackgroundColor;

        public void SetSelectedBackgroundColor(SolidColorBrush selectedColor)
        {
            _selectedBackgroundColor = selectedColor;
            _tabsViewModel.SetSelectedBackgroundColor(_selectedBackgroundColor);
        }

    }
}
