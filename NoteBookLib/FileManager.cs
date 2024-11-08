using NoteBookUI;

namespace NoteBookLib
{
    public class FileManager
    {
        private readonly Dictionary<string, IFileBuilder> _fileBuilders;

        public FileManager()
        {
            _fileBuilders = new Dictionary<string, IFileBuilder> {
                { ".txt", new TextDocumentBuilder() },
                { "", new NewTextDocumentBuilder() },
            };
        }

        public IDocument LoadFile(string filePath)
        {

            string extension = Path.GetExtension(filePath).ToLower();

            return _fileBuilders[extension].MakeDocument(filePath);

        }

    }
}
