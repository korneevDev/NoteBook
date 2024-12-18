﻿using NoteBookUI.View;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace NoteBookUI.ViewModel
{
    public class TabsViewModel
    {
        private readonly ObservableCollection<FileView> _tabs;

        public TabsViewModel()
        {
            _tabs = [];
            App.LanguageChanged += UpdateTabTitles;
            App.IntervalChanged += UpdateAutosaveInterval;
        }

        public ObservableCollection<FileView> GetTabsList() => _tabs;

        private void UpdateTabTitles()
        {
            foreach (var tab in _tabs)
            {
                tab.UpdateTitle();
            }
        }

        private void UpdateAutosaveInterval()
        {
            var interval = Settings.Default.AutoSaveInterval ?? "";

            foreach (var tab in _tabs)
            {
                tab.UpdateAutosaveInterval(interval);
                
            }
        }

        public void SetSelectedFont(FontFamily selectedFont)
        {
            foreach (var tab in _tabs)
            {
                tab.UpdateFont(selectedFont);
            }
        }

        public void SetSelectedFontSize(double selectedFontSize)
        {
            foreach (var tab in _tabs)
            {
                tab.UpdateFontSize(selectedFontSize);
            }
        }

        public void SetSelectedTextColor(SolidColorBrush selectedColor)
        {
            foreach (var tab in _tabs)
            {
                tab.UpdateTextColor(selectedColor);
            }
        }

        public void SetSelectedBackgroundColor(SolidColorBrush selectedColor)
        {
            foreach (var tab in _tabs)
            {
                tab.UpdateBackgroundColor(selectedColor);
            }
        }

        public void AddTab(FileView tab) { 
            _tabs.Add(tab);
        }

        public void RemoveTab(FileView tab)
        {
            tab.Dispose();
            _tabs.Remove(tab);
        }

        public bool CanExecuteTabCommand(object parameter) => parameter is FileView;
    }
}
