using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (change != null)
            {
                undoStack.Push(change);
                redoStack.Clear();
            }
        }
        public bool IsRedoAvailable()
        {
            return redoStack.Count != 0;
        }

        public bool IsUndoAvailable()
        {
            return undoStack.Count != 0;
        }

    }
}
