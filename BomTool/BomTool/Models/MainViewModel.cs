
using NooneUI;
using Qml.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NooneUI.Services;
using NooneUI.Pdf;

namespace BomTool.Models
{
    /// <summary>
    /// Main view model for main window
    /// </summary>
    [AutoRegisterType]
    [Signal("openFolderDialogSignal")]
    public class MainViewModel : BindableBase
    {
        private string statusText = string.Empty;
        private bool isBusy = false;

        internal HistoryEntry HistoryEntry => Container.Get<HistoryEntry>();

        public Action<string> OnPdfDialogAccept { get; private set; }

        public Action OnPdfDialogRejected { get; private set; }

        /// <summary>
        /// Indicate if it is busy
        /// </summary>
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value, nameof(IsBusy));
        }

        /// <summary>
        /// Status Text
        /// </summary>
        public string StatusText
        {
            get => statusText;
            set => SetProperty(ref statusText, value, nameof(StatusText));
        }

        public MainViewModel() => this.Paths = HistoryEntry.Read().ToList();

        /// <summary>
        /// Opened xls file path
        /// </summary>
        public List<string> Paths { get; } = new List<string>();

        /// <summary>
        /// Bom Data Read
        /// </summary>
        public IEnumerable<BomData> DataRead { get; private set; } = new List<BomData>();

        /// <summary>
        /// Bom data groupped
        /// </summary>
        public IEnumerable<GrouppedBomData> GrouppedData { get; private set; } = new List<GrouppedBomData>();

        /// <summary>
        /// All data from excel
        /// </summary>
        public ExcelData ExcelDataRead { get; private set; }

        /// <summary>
        /// Read excel async
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Task ReadAsync(string path) => Task.Factory.StartNew(() =>
        {
            try
            {
                var uri = new Uri(path);
                path = uri.LocalPath;
                this.IsBusy = true;
                WaitSometime();
                var reader = Container.Get<ExcelReader>();
                reader.Initialize(path, msg => this.StatusText = msg);
                this.ExcelDataRead = reader.Read();
                this.DataRead = ExcelDataRead.BomData;
                if (this.DataRead.Count() == 0)
                {
                    Logger.Debug("No data read.");
                    this.StatusText = "No suitable data present";
                    return;
                }
                AddPath(path);
            }
            catch (Exception ex)
            {
                Logger.Error("Open xls failed", ex);
                this.StatusText = $"Open {path} failed";
            }

        });

        /// <summary>
        /// Write excel async
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Task WriteAsync(string path) => Task.Factory.StartNew(() =>
        {
            if (this.DataRead.Count() == 0)
            {
                this.StatusText = "No data present, please choose a bom xls first.";
                return;
            }
            try
            {
                var uri = new Uri(path);
                path = uri.LocalPath;
                this.IsBusy = true;
                WaitSometime();
                var writer = Container.Get<ExcelWriter>();
                writer.Initialize(ExcelDataRead.BomData, path, msg => this.StatusText = msg);
                writer.Write();
                this.GrouppedData = writer.GrouppedData;
            }
            catch (Exception ex)
            {
                Logger.Error("Generate BOM Failed", ex);
                this.StatusText = "Generate BOM failed";
            }
        });

        /// <summary>
        /// Add xls path
        /// </summary>
        /// <param name="path"></param>
        public void AddPath(string path)
        {

            if (!Paths.Contains(path))
            {
                Paths.Add(path);
                HistoryEntry.Write(Paths);
            }
        }

        /// <summary>
        /// Clear history
        /// </summary>
        public void ClearHistory()
        {
            this.Paths.Clear();
            HistoryEntry.Write(Paths);
        }

        /// <summary>
        /// Preview pdf
        /// </summary>
        /// <param name="path"></param>
        public void PreviewPdf()
        {
            try
            {
                if (DataRead.Count() == 0)
                {
                    this.StatusText = "No data";
                    return;
                }
                this.IsBusy = true;

                var generator = Container.Get<PdfDataGenerator>();
                generator.Initialize(ExcelDataRead, msg => this.StatusText = msg);
                var data = generator.Generate();

                Container.Get<PdfPreviewer>().Preview(data,
                    () => this.ActivateSignal("openFolderDialogSignal"),
                    onAccept => this.OnPdfDialogAccept = onAccept,
                    onRejected => this.OnPdfDialogRejected = onRejected);

                this.StatusText = "Genrate PDF Successfully";
            }
            catch (Exception ex)
            {
                Logger.Error("Preview PDF Failed", ex);
                this.StatusText = "Preview PDF failed";
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        public void GeneratePdf(string pdf) => this.OnPdfDialogAccept?.Invoke(pdf);

        public void CancelGenerate() => this.OnPdfDialogRejected?.Invoke();

        /// <summary>
        /// Simulate to wait some time
        /// </summary>
        void WaitSometime() => Thread.Sleep((int)(new Random().NextDouble() * 1500));
    }
}
