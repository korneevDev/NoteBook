namespace NoteBookLib
{
    public interface IExtensionProvider
    {
        public Dictionary<String, IFileHandler> getBuildersDictionary();

        public List<String> GetExtensionsTemplate();

    }

    public class BaseExtensionProvider : IExtensionProvider
    {
        private readonly Dictionary<String, IFileHandler> _dictionary = new()
        {
                { ".txt", new IFileHandler.TextDocumentHandler() },
                { "", new IFileHandler.NewTextDocumentHandler() },
            };


        public Dictionary<string, IFileHandler> getBuildersDictionary() =>_dictionary;

        public List<string> GetExtensionsTemplate() => _dictionary.Keys.ToList();
    }
}
