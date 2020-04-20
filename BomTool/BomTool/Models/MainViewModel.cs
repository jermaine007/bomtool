
using NooneUI;
using Qml.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NooneUI.Services;

namespace BomTool.Models
{
    [AutoRegisterType]
    public class MainViewModel : NotifyPropertyChangedBase
    {
        private string statusText = string.Empty;
        private bool isBusy = false;

        internal HistoryEntry HistoryEntry => Container.Get<HistoryEntry>();

        //[NotifySignal]
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value, nameof(IsBusy));
            //set
            //{
            //    if (isBusy == value)
            //    {
            //        return;
            //    }
            //    Dispatch(() =>
            //    {
            //        isBusy = value;
            //        this.ActivateNotifySignal(nameof(IsBusy));
            //    });
            //}
        }

        //[NotifySignal]
        public string StatusText
        {
            get => statusText;

            //set
            //{
            //    if (statusText == value)
            //    {
            //        return;
            //    }


            //    Dispatch(() =>
            //    {
            //        statusText = value;
            //        this.ActivateNotifySignal(nameof(StatusText));
            //    });
            //}
            set => SetProperty(ref statusText, value, nameof(StatusText));
        }

        public MainViewModel()
        {
            this.Paths = HistoryEntry.Read().ToList();
        }

        public List<string> Paths { get; } = new List<string>();

        public IEnumerable<ExcelData> DataRead { get; private set; } = new List<ExcelData>();

        public IEnumerable<ExcelGrouppedData> GrouppedData { get; private set; } = new List<ExcelGrouppedData>();

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
                this.DataRead = reader.Read();
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
                writer.Initialize(DataRead, path, msg => this.StatusText = msg);
                writer.Write();
                this.GrouppedData = writer.GrouppedData;
            }
            catch (Exception ex)
            {
                Logger.Error("Generate BOM Failed", ex);
                this.StatusText = "Generate BOM failed";
            }
        });

        public void AddPath(string path)
        {

            if (!Paths.Contains(path))
            {
                Paths.Add(path);
                HistoryEntry.Write(Paths);
            }
        }

        public void ClearHistory()
        {
            this.Paths.Clear();
            HistoryEntry.Write(Paths);
        }

        void WaitSometime() => Thread.Sleep((int)(new Random().NextDouble() * 1500));
    }
}
