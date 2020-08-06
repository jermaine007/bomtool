using NooneUI.Models;
using NooneUI.ViewModels;
using System.Threading.Tasks;

namespace NooneUI.Framework
{
    internal class LightMessageBox : IMessageBox, IBaseServiceProvider
    {

        private readonly IContainer container;

        public LightMessageBox() => container = ((IBaseServiceProvider)this).Container;

        public Task<MessageBoxResults> ShowAsync(string message) =>
            container.Get<MessageBoxWindowViewModel>().With(messagebox =>
            {
                messagebox.Content = container.Get<MessageBoxViewModel>().With(view =>
                {
                    view.Settings = container.Get<MessageBoxSettings>().With(settings =>
                    {
                        settings.Message = message;
                    });
                });
            })
            .ShowDialog<MessageBoxResults>();


        public Task<MessageBoxResults> ShowAsync(string title, string message) =>
            container.Get<MessageBoxWindowViewModel>().With(messagebox =>
            {
                messagebox.Content = container.Get<MessageBoxViewModel>().With(view =>
                {
                    view.Settings = container.Get<MessageBoxSettings>().With(settings =>
                    {
                        settings.Message = message;
                        settings.Title = title;
                    });
                });

            })
            .ShowDialog<MessageBoxResults>();

        public Task<MessageBoxResults> ShowAsync(string title, string message, MessageBoxSettings settings) =>
            container.Get<MessageBoxWindowViewModel>().With(messagebox =>
            {
                messagebox.Content = container.Get<MessageBoxViewModel>().With(view =>
                {
                    settings.Title = title;
                    settings.Message = message;
                    view.Settings = settings;
                });

            })
            .ShowDialog<MessageBoxResults>();

        public Task<MessageBoxResults> ShowCustomizeAsync(object customView) =>
            container.Get<MessageBoxWindowViewModel>().With(messagebox =>
            {
                messagebox.Content = customView;
            })
            .ShowDialog<MessageBoxResults>();


        public Task<MessageBoxResults> ShowCustomizeAsync<TViewModel>() where TViewModel : IViewModel =>
            container.Get<MessageBoxWindowViewModel>().With(messagebox =>
            {
                messagebox.Content = container.Get<TViewModel>();
            })
            .ShowDialog<MessageBoxResults>();
    }
}
