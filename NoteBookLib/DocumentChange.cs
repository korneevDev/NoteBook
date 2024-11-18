namespace NoteBookLib
{
    public interface IDocumentChange
    {

        public void Undo(IDocument document);
        public void Redo(IDocument document);
    }

    public class RemoveTextChange : IDocumentChange
    {
        private readonly int _startIndex;
        private readonly string _removed;

        public RemoveTextChange(int startIndex, string removed)
        {
            _startIndex = startIndex;
            _removed = removed;
        }

        public void Undo(IDocument document)
        {
            document.AddText(_startIndex, _removed);
        }

        public void Redo(IDocument document)
        {
            document.RemoveText(_startIndex, _removed);
        }
    }

    public class AddTextChange : IDocumentChange
    {
        private readonly int _startIndex;
        public readonly string _added;

        public AddTextChange(int startIndex, string added)
        {
            _startIndex = startIndex;
            _added = added;
        }

        public void Undo(IDocument document)
        {
            document.RemoveText(_startIndex, _added);
        }

        public void Redo(IDocument document)
        {
            document.AddText(_startIndex, _added);
        }
    }
}
