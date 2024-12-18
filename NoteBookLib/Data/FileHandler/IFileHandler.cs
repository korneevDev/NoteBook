﻿
using System.Text;
using NoteBookLib.Entity.DataModel;

namespace NoteBookLib
{
    public interface IFileHandler
    {

        public Task<IDocument> MakeDocument(string filePath);

        public void SaveDocument(string filePath, string content);


        public class TextDocumentHandler : IFileHandler
        {
            public async Task<IDocument> MakeDocument(string filePath)
            {
                string content = await File.ReadAllTextAsync(filePath);
                return new DocumentModel(filePath, new IDocumentContent.TextContent(content));
            }

            public async void SaveDocument(string filePath, string content)
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    return;
                }
                using StreamWriter writer = new(filePath, false, Encoding.UTF8);
                await writer.WriteAsync(content);
            }
        }

        public class NewTextDocumentHandler : IFileHandler
        {
            public Task<IDocument> MakeDocument(string filePath)
            {
                return Task.FromResult<IDocument>(new DocumentModel());
            }
            
            public void SaveDocument(string filePath, string content)
            {
                throw new InvalidOperationException();
            }
        }
    } 
}
