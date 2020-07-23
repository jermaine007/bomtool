using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace NooneUI.Pdf.View
{
    public class NooneHtmlControl : System.Windows.Controls.ContentControl
    {
        readonly WindowsFormsHost host;

        public WebBrowser WebBrowser => host.Child as WebBrowser;

        // Using a DependencyProperty as the backing store for IsCompleted.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCompletedProperty =
            DependencyProperty.Register("IsCompleted", typeof(bool), typeof(NooneHtmlControl), new PropertyMetadata(false));

        // Using a DependencyProperty as the backing store for DataSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(object), typeof(NooneHtmlControl), new PropertyMetadata(null, OnDataSourceChanged));

        // Using a DependencyProperty as the backing store for HtmlDocument.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HtmlDocumentProperty =
            DependencyProperty.Register("HtmlDocument", typeof(string), typeof(NooneHtmlControl), new PropertyMetadata(null));


        public NooneHtmlControl()
        {
            this.host = new WindowsFormsHost();
            var browser = new WebBrowser() { ScrollBarsEnabled = false };
            host.Child = browser;
            this.Content = host;
        }

        public bool IsCompleted
        {
            get { return (bool)GetValue(IsCompletedProperty); }
            set { SetValue(IsCompletedProperty, value); }
        }

        public object DataSource
        {
            get { return GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        public string HtmlDocument
        {
            get { return (string)GetValue(HtmlDocumentProperty); }
            set { SetValue(HtmlDocumentProperty, value); }
        }

        private static void OnDataSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var htmlControl = d as NooneHtmlControl;
            htmlControl.OnDataSourceChanged(e);
        }

        protected virtual void OnDataSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            var webBrowser = this.WebBrowser;
            if (e.NewValue != null)
            {
                dynamic dataSource = (dynamic)e.NewValue;
                var template = dataSource.Template;
                var data = dataSource.Data;
                var html = new TemplateEngine().Render(template, data);
                this.HtmlDocument = html;
                this.IsCompleted = false;
                webBrowser.DocumentCompleted -= OnDocumentCompleted;
                webBrowser.DocumentCompleted += OnDocumentCompleted;
                Outputhtml(((object)html).ToString());
                webBrowser.DocumentText = html;
            }
        }

        private void OnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) => this.IsCompleted = true;

        [Conditional("DEBUG")]
        private static void Outputhtml(string content) => File.WriteAllText(
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "pdf.html"), content);
    }
}