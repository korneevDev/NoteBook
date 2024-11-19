using NoteBookUI.View;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace NoteBookUI.CommandHandlers
{
    public class FontCommandsHandler(MainViewModel mainViewModel) : OnPropertyChangedHandler
    {
        private readonly MainViewModel mainViewModel = mainViewModel;

        public ObservableCollection<FontFamily> AvailableFonts => mainViewModel.GetAvailableFonts();
        public ObservableCollection<double> AvailableFontSizes => mainViewModel.GetAvailableFontSizes();

        public FontFamily SelectedFont
        {
            get => mainViewModel.GetSelectedFont(); set
            {
                if (value != SelectedFont)
                {
                    mainViewModel.SetSelectedFont(value);
                    OnPropertyChanged(nameof(SelectedFont));
                }
            }
        }
        public double SelectedFontSize
        {
            get => mainViewModel.GetSelectedFontSize(); set
            {
                if (value != SelectedFontSize)
                {
                    mainViewModel.SetSelectedFontSize(value);
                    OnPropertyChanged(nameof(SelectedFontSize));
                }
            }
        }
    }
}
