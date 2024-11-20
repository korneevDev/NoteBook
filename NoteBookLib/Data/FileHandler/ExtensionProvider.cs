namespace NoteBookLib.Data.FileHandler
{
    public interface IExtensionProvider
    {
        public Dictionary<string, IFileHandler> GetBuildersDictionary();

        public List<string> GetExtensionsTemplate();



        public class Base() : IExtensionProvider
        {
            private readonly Dictionary<string, IFileHandler> _dictionary = new()
        {
                { ".txt", new IFileHandler.TextDocumentHandler() },
                { "", new IFileHandler.NewTextDocumentHandler() },
            };


            public Dictionary<string, IFileHandler> GetBuildersDictionary() => _dictionary;

            public List<string> GetExtensionsTemplate() => [.. _dictionary.Keys];
        }
    }
}
