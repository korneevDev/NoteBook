using System.Windows;

namespace NoteBookUI.Utils
{
    public static class StringResourceManager
    {
        public static string GetString(string key)
        {
            return Application.Current.Resources[key] as string ?? string.Empty;
        }
    }
}
