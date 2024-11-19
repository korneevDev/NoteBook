using NoteBookUI.View;


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
            var mainViewModel = new MainViewModel();

            FileCommands = new FileCommandsHandler(mainViewModel);

            EditCommands = new EditCommandsHandler(mainViewModel);

            ViewCommands = new ViewCommandsHandler(mainViewModel);

            ViewCommands = new ViewCommandsHandler(mainViewModel);

            FontsCommands = new FontCommandsHandler(mainViewModel);
        }

        
    }
}
