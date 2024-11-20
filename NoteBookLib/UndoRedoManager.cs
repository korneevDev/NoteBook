using NoteBookLib.DataModel;

namespace NoteBookLib
{
    public class UndoRedoManager
    {
        private readonly Stack<IDocumentChange> undoStack;
        private readonly Stack<IDocumentChange> redoStack;

        public UndoRedoManager()
        {
            undoStack = new Stack<IDocumentChange>();
            redoStack = new Stack<IDocumentChange>();
        }

        public void Clear()
        {
            undoStack.Clear();
            redoStack.Clear();
        }

        public void Undo(IDocument document)
        {
            IDocumentChange change = undoStack.Pop();
            change.Undo(document);
            redoStack.Push(change);
        }

        public void Redo(IDocument document)
        {
            IDocumentChange change = redoStack.Pop();
            change.Redo(document);
            undoStack.Push(change);
        }

        public void AddUndo(IDocumentChange change)
        {
            change.AddToStack(undoStack);
            redoStack.Clear();
        }

        public bool IsRedoAvailable() =>
            redoStack.Count != 0;
        

        public bool IsUndoAvailable() =>
            undoStack.Count != 0;
        

    }
}
