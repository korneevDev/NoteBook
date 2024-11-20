using NoteBookLib.DataModel;

namespace NoteBookLib.FeatureManager
{
    public class FindAndReplaceManager
    {
        private string lastSearched = "";
        private int index = -1;

        public void ClearCounter()
        {
            index = -1;
        }

        public void ReplaceAllText(string sourceText, string replaceText, IDocument _document) =>
            _document.ReplaceText(sourceText, replaceText);

        public void ReplaceText(string sourceText, string replaceText, IDocument _document)
        {
            if (index != -1)
                _document.ReplaceText(sourceText, replaceText, index);
        }

        public int FindText(string text, IDocument document)
        {
            if (lastSearched != text)
            {
                lastSearched = text;
                index = -1;
            }

            int findedIndex = document.FindSubstringIndexes(lastSearched, index + 1);

            if (findedIndex != -1)
            {
                index = findedIndex;
            }

            if (findedIndex == -1 && index != -1)
            {
                index = -1;
                findedIndex = document.FindSubstringIndexes(lastSearched, index + 1);
                index = findedIndex;
            }

            return findedIndex;
        }
    }
}
