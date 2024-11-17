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

        private FixedDocument _fixedDocument;
        public PrintWindow(IDocument document)
        {
            InitializeComponent();

            // Создаём FixedDocument
            _fixedDocument = CreateFixedDocument(document.Text());

            // Устанавливаем FixedDocument в DocumentViewer
            documentViewer.Document = _fixedDocument;
        }

        private FixedDocument CreateFixedDocument(string text)
        {
            // Создаём документ
            FixedDocument fixedDocument = new FixedDocument();

            // Создаём страницу
            FixedPage page = new FixedPage();
            page.Width = 816;  // Ширина страницы (8.5 inches)
            page.Height = 1056; // Высота страницы (11 inches)

            // Добавляем текст
            TextBlock textBlock = new TextBlock
            {
                Text = text,
                FontSize = 14,
                FontFamily = new FontFamily("Arial"),
                TextWrapping = TextWrapping.Wrap,
                Width = 750 // Ширина текста на странице
            };

            // Добавляем текстовый блок на страницу
            page.Children.Add(textBlock);

            // Оборачиваем страницу в PageContent
            PageContent pageContent = new PageContent();
            ((IAddChild)pageContent).AddChild(page);

            // Добавляем PageContent в FixedDocument
            fixedDocument.Pages.Add(pageContent);

            return fixedDocument;
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                // Печать документа
                printDialog.PrintDocument(_fixedDocument.DocumentPaginator, "Printing Preview Document");
                Close(); // Закрываем окно предпросмотра
            }
        }
    }
}
