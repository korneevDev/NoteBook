namespace NoteBookLib.FeatureManager
{
    public class ClipboardInteractor
    {
        private readonly List<string> _buffer;

        public ClipboardInteractor()
        {
            _buffer = [];
        }

        public void Copy(string text)
        {
            _buffer.Add(text);
        }

        public string GetLastValueFromBuffer() =>_buffer[^1];
        

        public List<string> GetBuffer() => _buffer;

        public void SetOldValueToBufferTop(int index)
        {
            _buffer.Add(_buffer[index]);
            _buffer.RemoveAt(index);
        }
        public bool IsInsertAvailable() =>
            _buffer.Count != 0;

    }
}
