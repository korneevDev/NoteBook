using NoteBookUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBookLib
{
    public interface IFileBuilder
    {

        public IDocument MakeDocument(string filePath);
    }

    public class TextDocumentBuilder : IFileBuilder
    {
        public IDocument MakeDocument(string filePath)
        {
            return new TextDocumentModel(filePath, File.ReadAllText(filePath));
        }
      
    }

    public class NewTextDocumentBuilder : IFileBuilder
    {
        public IDocument MakeDocument(string filePath)
        {
            return new TextDocumentModel();
        }

    }
}
