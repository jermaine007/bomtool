using GalaSoft.MvvmLight.Command;
using NooneUI.Pdf.View;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NooneUI.Pdf.ViewModel
{
    [ViewLocator(typeof(PreviewWindow))]
    public class PreviewWindowViewModel : BaseWindowViewModel
    {
        private ICommand cmdGenerate;
        private string document;
        private bool isCompleted;
        private object dataSource;

        public Action PreviewCallback { get; set; }

        public object DataSource
        {
            get => dataSource;
            private set => Set(ref dataSource, value);
        }

        public string Document
        {
            get => document;
            set => Set(ref document, value);
        }

        public bool IsCompleted
        {
            get => isCompleted;
            set => Set(ref isCompleted, value);
        }

        public ICommand CmdGenerate => cmdGenerate ??= new RelayCommand(PreviewCallback, () => this.IsCompleted && !string.IsNullOrEmpty(Document));

        public Task GeneratePdf(string path)
        {
            this.Close();
            return Task.Factory.StartNew(() =>
            {
                path = new Uri(path).LocalPath;

                var pdfFile = Path.Combine(path,
                    $"Bom-{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.pdf");

                // instantiate a html to pdf converter object
                var converter = new HtmlToPdf();

                // set converter options
                converter.Options.PdfPageSize = PdfPageSize.A3;
                converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
                //converter.Options.WebPageFixedSize = true;
                //converter.Options.WebPageHeight = 842;
                //converter.Options.WebPageWidth = 1191;
                converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
                converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
                // create a new pdf document converting an html string
                var doc = converter.ConvertHtmlString(this.Document);

                // save pdf document
                doc.Save(pdfFile);

                // close pdf document
                doc.Close();

                Process.Start(new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = pdfFile
                });
            });
        }

        public Task Bind(object dataSource) =>
            Task.Factory.StartNew(() =>
            {
                this.DataSource = dataSource;
                while (!IsCompleted)
                {
                    Task.Delay(1000);
                }
                Application.Current.Dispatcher.Invoke(() =>
                  (this.CmdGenerate as RelayCommand).RaiseCanExecuteChanged());
            });


    }
}
