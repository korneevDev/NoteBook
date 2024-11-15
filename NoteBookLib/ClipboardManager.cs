using System.Xml.Linq;

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

        public string GetLastValueFromBuffer()
        {
            return _buffer[^1];
        }

        public List<string> GetBuffer() => _buffer;

        public void SetOldValueToBufferTop(int index)
        {
            _buffer.Add(_buffer[index]);
            _buffer.RemoveAt(index);
        }

    }
}
