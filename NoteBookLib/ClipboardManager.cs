namespace NoteBookLib
{
    public class ClipboardManager
    {
        private readonly List<string> _buffer;
        
        public ClipboardManager()
        {
            _buffer = [];
        }

        public void Copy(string text)
        {
            _buffer.Add(text);
        }

        public string GetBuffer()
        {
            return _buffer[^1];
        }

    }
}
