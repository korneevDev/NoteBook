namespace NoteBookLib.FileHandler
{
    public interface IExtensionProvider
    {
        public Dictionary<string, IFileHandler> getBuildersDictionary();

        public List<string> GetExtensionsTemplate();



        public class Base() : IExtensionProvider
        {
            private readonly Dictionary<string, IFileHandler> _dictionary = new()
        {
                { ".txt", new IFileHandler.TextDocumentHandler() },
                { "", new IFileHandler.NewTextDocumentHandler() },
            };


            public Dictionary<string, IFileHandler> getBuildersDictionary() => _dictionary;

            public List<string> GetExtensionsTemplate() => _dictionary.Keys.ToList();
        }
    }
}
