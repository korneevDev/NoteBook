namespace NoteBookLib.Entity.DataModel
{
    public interface IDocumentChange
    {

        public void Undo(IDocument document);
        public void Redo(IDocument document);

        public void AddToStack(Stack<IDocumentChange> stack);


        public class RemoveTextChange(int startIndex, string removed) : IDocumentChange
        {
            private readonly int _startIndex = startIndex;
            private readonly string _removed = removed;

            public void Undo(IDocument document)
            {
                document.AddText(_startIndex, _removed);
            }

            public void Redo(IDocument document)
            {
                document.RemoveText(_startIndex, _removed);
            }

            public void AddToStack(Stack<IDocumentChange> stack) => stack.Push(this);
        }

        public class AddTextChange(int startIndex, string added) : IDocumentChange
        {
            private readonly int _startIndex = startIndex;
            public readonly string _added = added;

            public void Undo(IDocument document)
            {
                document.RemoveText(_startIndex, _added);
            }

            public void Redo(IDocument document)
            {
                document.AddText(_startIndex, _added);
            }

            public void AddToStack(Stack<IDocumentChange> stack) => stack.Push(this);
        }

        public class EmptyChange : IDocumentChange
        {
            public void AddToStack(Stack<IDocumentChange> stack) { }

            public void Redo(IDocument document) =>
                throw new NotSupportedException();


            public void Undo(IDocument document) =>
                throw new NotSupportedException();

        }
    }
}
