using NoteBookLib;
using NoteBookLib.DataModel;
using NoteBookUI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace NoteBookUI
{
    /// <summary>
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : Window
    {

        private readonly FixedDocument _fixedDocument;
        public PrintWindow(
            Printer printer,
            FileView tab, 
            FontFamily font, 
            double fontSize, 
            SolidColorBrush textColor, 
            SolidColorBrush backgroundColor
            )
        {
            InitializeComponent();

            tab.PrintContent(printer);

            _fixedDocument = printer.CreateFixedDocument(font, fontSize, textColor, backgroundColor);

            documentViewer.Document = _fixedDocument;
        }

    }

    public class Printer : IPrinter
    {
        private string? _text;
        public void Print(string text)
        {
            _text = text;
        }

        public FixedDocument CreateFixedDocument(
            FontFamily font,
            double fontSize,
            SolidColorBrush textColor,
            SolidColorBrush backgroundColor)
        {
            // Создаём документ
            FixedDocument fixedDocument = new();

            // Создаём страницу
            FixedPage page = new()
            {
                Width = 816,  // Ширина страницы (8.5 inches)
                Height = 1056 // Высота страницы (11 inches)
            };

            // Добавляем текст
            TextBlock textBlock = new()
            {
                Text = _text ?? "",
                FontSize = fontSize,
                FontFamily = font,
                TextWrapping = TextWrapping.Wrap,
                Foreground = textColor,
                Background = backgroundColor,
                Width = 750 // Ширина текста на странице
            };

            // Добавляем текстовый блок на страницу
            page.Children.Add(textBlock);

            // Оборачиваем страницу в PageContent
            PageContent pageContent = new();
            ((IAddChild)pageContent).AddChild(page);

            // Добавляем PageContent в FixedDocument
            fixedDocument.Pages.Add(pageContent);

            return fixedDocument;
        }
    }
}
