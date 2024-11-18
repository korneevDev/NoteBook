namespace NoteBookLib
{
    public interface IExtensionProvider
    {
        public Dictionary<String, IFileHandler> getBuildersDictionary();

        public List<String> GetExtensionsTemplate();

    }

    public class BaseExtensionProvider : IExtensionProvider
    {
        private Dictionary<String, IFileHandler> _dictionary = new()
        {
                { ".txt", new TextDocumentHandler() },
                { "", new NewTextDocumentHandler() },
            };


        public Dictionary<string, IFileHandler> getBuildersDictionary() =>_dictionary;

        public List<string> GetExtensionsTemplate() => _dictionary.Keys.ToList();
    }
}
