using NoteBookLib.Entity.DataModel;

namespace NoteBookLib.Domain.FeatureInteractor
{
    public class AutoSaveInteractor(FileInteractor fileManager) : IDisposable
    {
        private Timer? _autoSaveTimer; // Таймер для автосохранения
        private readonly FileInteractor _fileManager = fileManager;
        private IDocument? _document;

        public void Start(int interval) =>
            _autoSaveTimer = new Timer(
                callback: AutoSaveCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(interval)
            );

        public void UpdateTimer(string interval, IDocument document)
        {
            _document = document;
            if (int.TryParse(interval, out int intInterval))
            {
                Start(intInterval);
                return;
            }

            StopTimer();

        }

        // Обработчик события Elapsed для автосохранения
        private void AutoSaveCallback(object? state)
        {
            try
            {   if (_document != null) 
                    _fileManager.SaveFile(_document);
            }
            catch (Exception ex)
            {
                // Логирование ошибок
                Console.WriteLine($"Ошибка автосохранения: {ex.Message}");
            }
        }


        private void StopTimer()
        {
            _autoSaveTimer?.Dispose();
            _autoSaveTimer = null;
        }

        public void Dispose()
        {
            StopTimer();    
            _fileManager.Dispose();
            _document = null;
        }
    }
}

