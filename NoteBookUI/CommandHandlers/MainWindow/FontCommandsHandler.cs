using NoteBookUI.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace NoteBookUI.CommandHandlers
{
    public class FontCommandsHandler(FontViewModel _fontViewModel) : OnPropertyChangedHandler
    {

        public ObservableCollection<FontFamily> AvailableFonts => _fontViewModel.GetAvailableFonts();
        public ObservableCollection<double> AvailableFontSizes => _fontViewModel.GetAvailableFontSizes();

        public ObservableCollection<SolidColorBrush> AvailableColors => _fontViewModel.GetAvailableColors();


        public FontFamily SelectedFont
        {
            get => _fontViewModel.GetSelectedFont(); 
            set
            {
                if (value != SelectedFont)
                {
                    _fontViewModel.SetSelectedFont(value);
                    OnPropertyChanged(nameof(SelectedFont));
                }
            }
        }
        public double SelectedFontSize
        {
            get => _fontViewModel.GetSelectedFontSize(); 
            set
            {
                if (value != SelectedFontSize)
                {
                    _fontViewModel.SetSelectedFontSize(value);
                    OnPropertyChanged(nameof(SelectedFontSize));
                }
            }
        }

        public SolidColorBrush SelectedTextColor
        {
            get => _fontViewModel.GetSelectedTextColor(); 
            set
            {
                if (value != SelectedTextColor)
                {
                    _fontViewModel.SetSelectedTextColor(value);
                    OnPropertyChanged(nameof(SelectedTextColor));
                }
            }
        }

        public SolidColorBrush SelectedBackgroundColor
        {
            get => _fontViewModel.GetSelectedTextColor();
            set
            {
                if (value != SelectedBackgroundColor)
                {
                    _fontViewModel.SetSelectedBackgroundColor(value);
                    OnPropertyChanged(nameof(SelectedBackgroundColor));
                }
            }
        }
    }
}
