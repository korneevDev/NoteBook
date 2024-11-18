

namespace NoteBookLib
{
    public class AutoSaveManager
    {
        private Timer autoSaveTimer; // Таймер для автосохранения
        private TextEditor textEditor;


        public AutoSaveManager(TextEditor textEditor)
        {
            this.textEditor = textEditor;
            
        }

        public void Start()
        {
            autoSaveTimer = new Timer(AutoSaveCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        // Обработчик события Elapsed для автосохранения
        private void AutoSaveCallback(object state)
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

        
        // Остановка таймера при закрытии окна
        private void Window_Closed(object sender, EventArgs e)
        {
            autoSaveTimer.Dispose(); // Останавливаем таймер
        }
    }
}

