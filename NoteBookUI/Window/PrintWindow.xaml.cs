using NoteBookLib;
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

namespace NoteBookUI
{
    /// <summary>
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : Window
    {

        private readonly FixedDocument _fixedDocument;
        private readonly FontFamily _font;
        private readonly double _fontSize;
        private readonly SolidColorBrush _textColor;
        private readonly SolidColorBrush _backgroundColor;
        public PrintWindow(
            IDocument document, 
            FontFamily font, 
            double fontSize, 
            SolidColorBrush textColor, 
            SolidColorBrush backgroundColor
            )
        {
            InitializeComponent();

            // Создаём FixedDocument
            _fixedDocument = CreateFixedDocument(document.Text());
            _font = font;
            _fontSize = fontSize;

            // Устанавливаем FixedDocument в DocumentViewer
            documentViewer.Document = _fixedDocument;
        }

        private FixedDocument CreateFixedDocument(string text)
        {
            // Создаём документ
            FixedDocument fixedDocument = new();

            // Создаём страницу
            FixedPage page = new FixedPage
            {
                Width = 816,  // Ширина страницы (8.5 inches)
                Height = 1056 // Высота страницы (11 inches)
            };

            // Добавляем текст
            TextBlock textBlock = new()
            {
                Text = text,
                FontSize = _fontSize,
                FontFamily = _font,
                TextWrapping = TextWrapping.Wrap,
                Foreground = _textColor,
                Background = _backgroundColor,
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
