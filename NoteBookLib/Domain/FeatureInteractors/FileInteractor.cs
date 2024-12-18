﻿using NoteBookLib.Data.FileHandler;
using NoteBookLib.Entity.DataModel;

namespace NoteBookLib.Domain.FeatureInteractor
{
    public class FileInteractor(
        IExtensionProvider extensionProvider, 
        IPathFormatter pathFormatter
        )
    {
        private IExtensionProvider _extensionProvider = extensionProvider;
        private IPathFormatter _pathFormatter = pathFormatter;
        private Action _updateTitleCallback = () => { };

        public void SetOnUpdateTitleCallback(Action updateTitleCallback)
        {
            _updateTitleCallback = updateTitleCallback;
        }
        public async Task<IDocument> LoadFile(string filePath)
        {

            string extension = Path.GetExtension(filePath).ToLower();

            return await _extensionProvider.GetBuildersDictionary()[extension].MakeDocument(filePath);

        }

        public void SaveFile(string filePath, IDocument document)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            document.Save(filePath, _extensionProvider.GetBuildersDictionary()[extension]);

            _updateTitleCallback.Invoke();

        }

        public void SaveFile(IDocument document)
        {
            string extension = document.GetExtension();

            document.Save(_extensionProvider.GetBuildersDictionary()[extension]);

            _updateTitleCallback.Invoke();

        }

        public List<string> GetAvailableExtensions() =>
            _extensionProvider.GetExtensionsTemplate();

        public string FormatRepitedPath(string path) =>
            _pathFormatter.FormatRepitedPath(path);

        public void Dispose()
        {
            _extensionProvider = null;
            _pathFormatter = null;
            _updateTitleCallback = null;
        }
    }
}
