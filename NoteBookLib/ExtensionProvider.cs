namespace NoteBookLib
{
    public interface IExtensionProvider
    {
        public Dictionary<String, IFileHandler> getBuildersDictionary();

        public String GetExtensionsTemplate();

    }

    public class BaseExtensionProvider : IExtensionProvider
    {
        private Dictionary<String, IFileHandler> _dictionary = new()
        {
                { ".txt", new TextDocumentHandler() },
                { "", new NewTextDocumentHandler() },
            };


public Dictionary<string, IFileHandler> getBuildersDictionary() =>_dictionary;

        public string GetExtensionsTemplate()
        {
            throw new NotImplementedException();
        }
    }
}
