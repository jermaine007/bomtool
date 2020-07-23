
using NooneUI.Pdf.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NooneUI.Pdf
{
    public class PdfPreviewer : Application, IDisposable
    {
        static PdfPreviewer() => Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        public PdfPreviewer() =>
            // If not this setting, would give error "The URI prefix is not recognized"
            // https://www.cnblogs.com/wyong27/archive/2009/03/29/1424389.html
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

        public void Dispose() => Application.Current.Shutdown();


        public void Preview(object dataSource = null, 
            Action previewCallback = null, 
            Action<Action<string>> onAccept = null, 
            Action<Action> onRejected = null)
        {
            var vm = new PreviewWindowViewModel();
            
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var template = ((dynamic)dataSource).UseSingleTemplate ? Path.Combine(baseDir, @"Template\Pdf.html") :
                Path.Combine(baseDir, @"Template\MultiPdf.html");
            
            vm.PreviewCallback = previewCallback;
            onAccept(new Action<string>(async (path) => await vm.GeneratePdf(path)));
            onRejected(new Action(vm.BringToFront));
            
            vm.Bind(new { Template = template, Data = dataSource });
            vm.Show();
        }
    }
}
