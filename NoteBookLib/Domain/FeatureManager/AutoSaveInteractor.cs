using NoteBookLib.Presentation;

namespace NoteBookLib.Domain.FeatureManager
{
    public class AutoSaveInteractor(TextEditor textEditor) : IDisposable
    {
        private Timer? autoSaveTimer; // Таймер для автосохранения
        private readonly TextEditor textEditor = textEditor;

        public void Start(int interval) =>
            autoSaveTimer = new Timer(
                callback: AutoSaveCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(interval)
            );

        public void UpdateInterval(string interval)
        {
            if (int.TryParse(interval, out int intInterval))
            {
                Start(intInterval);
                return;
            }

            Dispose();

        }

        // Обработчик события Elapsed для автосохранения
        private void AutoSaveCallback(object? state)
        {
            try
            {
                textEditor.SaveFile();
            }
            catch (Exception ex)
            {
                // Логирование ошибок
                Console.WriteLine($"Ошибка автосохранения: {ex.Message}");
            }
        }


        // Остановка таймера
        public void Dispose()
        {
            autoSaveTimer?.Dispose(); // Останавливаем таймер
            autoSaveTimer = null;
        }
    }
}

