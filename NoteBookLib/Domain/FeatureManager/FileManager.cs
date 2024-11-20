using NoteBookLib.Data.FileHandler;
using NoteBookLib.Entity.DataModel;

namespace NoteBookLib.Domain.FeatureManager
{
    public class FileManager(
        IExtensionProvider extensionProvider, 
        IPathFormatter pathFormatter
        )
    {
        private readonly IExtensionProvider _extensionProvider = extensionProvider;
        private readonly IPathFormatter _pathFormatter = pathFormatter;

        public async Task<IDocument> LoadFile(string filePath)
        {

            string extension = Path.GetExtension(filePath).ToLower();

            return await _extensionProvider.getBuildersDictionary()[extension].MakeDocument(filePath);

        }

        public async void SaveFile(string filePath, IDocument document)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            document.Save(filePath, _extensionProvider.getBuildersDictionary()[extension]);

        }

        public void SaveFile(IDocument document)
        {
            string extension = document.GetExtension();

            document.Save(_extensionProvider.getBuildersDictionary()[extension]);

        }

        public List<string> GetAvailableExtensions() =>
            _extensionProvider.GetExtensionsTemplate();

        public string Format(string path) =>
            _pathFormatter.Format(path);
        
    }
}
