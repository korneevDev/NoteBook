using NoteBookLib;
using NoteBookUI.ViewModel;


namespace NoteBookUI.CommandHandlers
{
    public class MainComandHandler : OnPropertyChangedHandler
    {

        public FileCommandsHandler FileCommands { get; }
        public EditCommandsHandler EditCommands { get; } 
        public ViewCommandsHandler ViewCommands { get; }
        public FontCommandsHandler FontsCommands { get; }

        public MainComandHandler()
        {
            var pathFormatter = new IPathFormatter.Base();
            var clipboardManager = new ClipboardManager();
            var printer = new Printer();

            var tabsViewModel = new TabsViewModel();
            var fontViewModel = new FontViewModel(tabsViewModel, printer);
            var fileHandlerViewModel = 
                new FileHandlerViewModel(
                    fontViewModel, clipboardManager, 
                    pathFormatter, tabsViewModel
                );

            var openWindowDialogViewModel = 
                new OpenWindowDialogViewModel(
                    fileHandlerViewModel, 
                    fontViewModel, 
                    clipboardManager
                );

            var editFileViewModel = new EditFileViewModel();

            FileCommands = new FileCommandsHandler(fileHandlerViewModel, tabsViewModel);

            EditCommands = new EditCommandsHandler(editFileViewModel, tabsViewModel);

            ViewCommands = new ViewCommandsHandler(openWindowDialogViewModel, tabsViewModel);

            FontsCommands = new FontCommandsHandler(fontViewModel);
        }

        
    }
}
