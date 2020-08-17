using BomTool.NetCore.Models;
using Noone.UI;
using Noone.UI.Core;
using Noone.UI.Models;
using Noone.UI.ViewModels;
using ReactiveUI;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BomTool.NetCore.ViewModels
{
    public class TabContentViewModel : ViewModelBase, ITemplateEngineProvider
    {

        private MainWindowViewModel mainWindow;
        private readonly ITemplateEngine templateEngine;
        private readonly string singleTemplate;
        private readonly string multiTemplate;

        private string header = string.Empty;
        private object content;
        private string location = string.Empty;
        private bool canGeneratePdf = false;

        public string Header
        {
            get => header;
            set => this.RaiseAndSetIfChanged(ref header, value);
        }

        public object Content
        {
            get => content;
            set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public string Location
        {
            get => location;
            set => this.RaiseAndSetIfChanged(ref location, value);
        }

        public bool CanGeneratePdf
        {
            get => canGeneratePdf;
            set => this.RaiseAndSetIfChanged(ref canGeneratePdf, value);
        }

        public MainWindowViewModel MainWindow => mainWindow ??= container.Get<MainWindowViewModel>();
        public ReactiveCommand<Unit, Unit> GenerateBomCommand { get; }
        public ReactiveCommand<Unit, Unit> GeneratePdfCommand { get; }
        public ExcelData ExcelData { get; set; }

        public TabContentViewModel()
        {
            templateEngine = ((ITemplateEngineProvider)this).TemplateEngine;
            singleTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template", "Pdf.html.template");
            multiTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template", "MultiPdf.html.template");

            GenerateBomCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                string folder = await dialog.OpenFolderDialogAsync("Select a folder");
                if (string.IsNullOrEmpty(folder))
                {
                    return;
                }
                MainWindow.Waiting((statusBar) =>
                {
                    ExcelWriter writer = container.Get<ExcelWriter>().Setup(o =>
                    {
                        o.Initialize(ExcelData.BomData, folder, msg => statusBar.Message = msg);
                    });
                    writer.Write();
                    this.Content = container.Get<GeneratedBomDataViewModel>().Setup(vm =>
                    {
                        vm.Initialize(writer.GrouppedData);
                        this.CanGeneratePdf = true;
                    });
                });
            });

            GeneratePdfCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                string folder = await dialog.OpenFolderDialogAsync("Select a folder");
                if (string.IsNullOrEmpty(folder))
                {
                    return;
                }
                MainWindow.Waiting((statusBar) =>
                {
                    PdfDataGenerator generator = container.Get<PdfDataGenerator>().Setup(o =>
                    {
                        o.Initialize(ExcelData, msg => statusBar.Message = msg);
                    });
                    PdfData pdf = generator.Generate();
                    GeneratePdf(pdf, folder);
                });

            });
        }

        private async void GeneratePdf(PdfData data, string folder)
        {
            var template = data.UseSingleTemplate ? singleTemplate : multiTemplate;
            string html = templateEngine.Render(template, data);
            var pdfFile = Path.Combine(folder,
                   $"Bom-{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.pdf");

            // instantiate a html to pdf converter object
            var converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = PdfPageSize.A3;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
            // create a new pdf document converting an html string
            var doc = converter.ConvertHtmlString(html);

            // save pdf document
            doc.Save(pdfFile);

            // close pdf document
            doc.Close();

            MessageBoxResults result = await messagebox.ShowAsync("", "Do you want to preview the pdf file?", container.Get<MessageBoxSettingsViewModel>().Setup(o =>
            {
                o.Buttons = MessageBoxButtons.YesNo;
            }));
            if (result == MessageBoxResults.Yes)
            {
                Process.Start(new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = pdfFile
                });
            }
        }
    }
}
