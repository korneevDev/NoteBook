
namespace NoteBookLib
{
    public class FileManager
    {
        private readonly Dictionary<string, IFileHandler> _fileHandlers;

        public FileManager()
        {
            _fileHandlers = new Dictionary<string, IFileHandler> {
                { ".txt", new TextDocumentHandler() },
                { "", new NewTextDocumentHandler() },
            };
        }

        public IDocument LoadFile(string filePath)
        {

            string extension = Path.GetExtension(filePath).ToLower();

            return _fileHandlers[extension].MakeDocument(filePath);

        }

        public void SaveFile(string filePath, IDocument document)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            document.Save(filePath, _fileHandlers[extension]);

        }

        public void SaveFile(IDocument document)
        {
            string extension = document.GetExtension();

            document.Save(_fileHandlers[extension]);

        }

    }
}
