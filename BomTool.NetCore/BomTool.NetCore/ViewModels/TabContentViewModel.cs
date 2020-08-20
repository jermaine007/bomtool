using BomTool.NetCore.Models;
using Noone.UI;
using Noone.UI.Models;
using Noone.UI.ViewModels;
using ReactiveUI;
using SelectPdf;
using System;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Runtime.InteropServices;
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
                MainWindow.Waiting(async (statusBar, callback) =>
                {
                    ExcelWriter writer = container.Get<ExcelWriter>().Setup(o =>
                    {
                        o.Initialize(ExcelData.BomData, folder, msg => this.MainWindow.Output(msg));
                    });
                    await writer.Write();
                    this.Content = container.Get<GeneratedBomDataViewModel>().Setup(vm =>
                    {
                        vm.Initialize(writer.GrouppedData);
                        // workaround for nonwindows platform
                        this.CanGeneratePdf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                    });
                    callback();
                });
            });

            GeneratePdfCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                string folder = await dialog.OpenFolderDialogAsync("Select a folder");
                if (string.IsNullOrEmpty(folder))
                {
                    return;
                }
                MainWindow.Waiting(async (statusBar, callback) =>
                {
                    PdfDataGenerator generator = container.Get<PdfDataGenerator>().Setup(o =>
                    {
                        o.Initialize(ExcelData, msg => this.MainWindow.Output(msg));
                    });
                    PdfData pdf = generator.Generate();
                    var pdfFile = await GeneratePdf(pdf, folder);

                    MessageBoxResults result = await messagebox.ShowAsync("", "Do you want to preview the pdf file?",
                    container.Get<MessageBoxSettingsViewModel>().Setup(o =>
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
                    callback();
                });

            });
        }

        // libgdiplus not found error, refer to https://github.com/mono/libgdiplus
        // It's a piece of shit that Select.HtmlToPdf.NetCore is Windows only, WTF!!!
        // https://selectpdf.com/docs/Installation.htm
        // https://stackoverflow.com/questions/56275154/selectpdf-net-core-2-2-exception-conversion-failure-unable-to-load-shared-li
        private Task<string> GeneratePdf(PdfData data, string folder) => Task.Factory.StartNew(() =>
        {
            this.MainWindow.Output("Generating pdf file...");
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
            // create a new pdf document converting an html string
            var doc = converter.ConvertHtmlString(html);

            // save pdf document
            doc.Save(pdfFile);

            // close pdf document
            doc.Close();
            this.MainWindow.Output("Generate pdf file sucessfully.");
            return pdfFile;
        });

    }
}
