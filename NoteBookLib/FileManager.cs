
namespace NoteBookLib
{
    public class FileManager
    {
        private readonly IExtensionProvider extensionProvider;

        public FileManager()
        {
            extensionProvider = new BaseExtensionProvider();
        }

        public IDocument LoadFile(string filePath)
        {

            string extension = Path.GetExtension(filePath).ToLower();

            return extensionProvider.getBuildersDictionary()[extension].MakeDocument(filePath);

        }

        public void SaveFile(string filePath, IDocument document)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            document.Save(filePath, extensionProvider.getBuildersDictionary()[extension]);

        }

        public void SaveFile(IDocument document)
        {
            string extension = document.GetExtension();

            document.Save(extensionProvider.getBuildersDictionary()[extension]);

        }

    }
}
