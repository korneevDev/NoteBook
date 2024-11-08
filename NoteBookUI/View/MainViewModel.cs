using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TextEditorApp.ViewModels;
using NoteBookUI.View;


namespace NoteBookUI.ViewModels
{
    public class MainViewModel : OnPropertyChangedHandler
    {
        public ObservableCollection<TabItemExtended> Tabs { get; }

        public ICommand NewTabCommand { get; }
        public ICommand CloseTabCommand { get; }


        public MainViewModel()
        {
            NewTabCommand = new RelayCommand(CreateNewTab);
            Tabs = [];
            CloseTabCommand = new RelayCommand<TabItemExtended>(CloseTab);
        }

        private void CreateNewTab()
        {
            var newTabViewModel = new TabViewModel();
            var newTab = new TabItemExtended(newTabViewModel);
            Tabs.Add(newTab);
        }

        private void CloseTab(TabItemExtended tab)
        {
            if (tab == null)
                return;

            if (!tab.TabViewModel.CanRemove())
            {
                var result = MessageBox.Show(
                        "You have unsaved changes. Do you really want to close this tab?",
                        "Unsaved Changes",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

            Tabs.Remove(tab);

        }
    }
}
