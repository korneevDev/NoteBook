namespace NoteBookLib
{
    public interface IDocumentChange
    {

        public void Undo(IDocument document);
        public void Redo(IDocument document);
    }

    public class RemoveTextChange : IDocumentChange
    {
        public int StartIndex { get; }
        public string Removed { get; }

        public RemoveTextChange(int startIndex, string removed)
        {
            StartIndex = startIndex;
            Removed = removed;
        }

        public void Undo(IDocument document)
        {
            document.AddText(StartIndex, Removed);
        }

        public void Redo(IDocument document)
        {
            document.RemoveText(StartIndex, Removed);
        }
    }

    public class AddTextChange : IDocumentChange
    {
        public int StartIndex { get;}
        public string Added { get; }

        public AddTextChange(int startIndex, string added)
        {
            StartIndex = startIndex;
            Added = added;
        }

        public void Undo(IDocument document)
        {
            document.RemoveText(StartIndex, Added);
        }

        public void Redo(IDocument document)
        {
            document.AddText(StartIndex, Added);
        }
    }
}
