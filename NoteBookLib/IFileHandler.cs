
namespace NoteBookLib
{
    public interface IFileHandler
    {

        public IDocument MakeDocument(string filePath);

        public void SaveDocument(string filePath, string content);
    }

    public class TextDocumentHandler : IFileHandler
    {
        public IDocument MakeDocument(string filePath)
        {
            return new TextDocumentModel(filePath, File.ReadAllText(filePath));
        }

        public void SaveDocument(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }
    }

    public class NewTextDocumentHandler : IFileHandler
    {
        public IDocument MakeDocument(string filePath)
        {
            return new TextDocumentModel();
        }

        public void SaveDocument(string filePath, string content)
        {
            throw new InvalidOperationException();
        }
    }
}
