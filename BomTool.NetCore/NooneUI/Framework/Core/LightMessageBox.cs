using NooneUI.Models;
using NooneUI.ViewModels;
using System.Threading.Tasks;

namespace NooneUI.Framework
{
    [AutoRegister(Singleton = true, InterfaceType = typeof(IMessageBox))]
    internal class LightMessageBox : IMessageBox, IBaseServiceProvider, ICanAutoRegister
    {

        private readonly IContainer container;

        public LightMessageBox() => container = ((IBaseServiceProvider)this).Container;

        public Task<MessageBoxResults> ShowAsync(string message) =>
            container.Get<MessageBoxWindowViewModel>().Setup(messagebox =>
            {
                messagebox.Content = container.Get<MessageBoxViewModel>().Setup(view =>
                {
                    view.Settings = container.Get<MessageBoxSettingsViewModel>().Setup(settings =>
                    {
                        settings.Message = message;
                    });
                });
            })
            .ShowDialog<MessageBoxResults>();


        public Task<MessageBoxResults> ShowAsync(string title, string message) =>
            container.Get<MessageBoxWindowViewModel>().Setup(messagebox =>
            {
                messagebox.Content = container.Get<MessageBoxViewModel>().Setup(view =>
                {
                    view.Settings = container.Get<MessageBoxSettingsViewModel>().Setup(settings =>
                    {
                        settings.Message = message;
                        settings.Title = title;
                    });
                });

            })
            .ShowDialog<MessageBoxResults>();

        public Task<MessageBoxResults> ShowAsync(string title, string message, MessageBoxSettingsViewModel settings) =>
            container.Get<MessageBoxWindowViewModel>().Setup(messagebox =>
            {
                messagebox.Content = container.Get<MessageBoxViewModel>().Setup(view =>
                {
                    settings.Title = title;
                    settings.Message = message;
                    view.Settings = settings;
                });

            })
            .ShowDialog<MessageBoxResults>();

        public Task<MessageBoxResults> ShowCustomizeAsync(object customView) =>
            container.Get<MessageBoxWindowViewModel>().Setup(messagebox =>
            {
                messagebox.Content = customView;
            })
            .ShowDialog<MessageBoxResults>();


        public Task<MessageBoxResults> ShowCustomizeAsync<TViewModel>() where TViewModel : IViewModel =>
            container.Get<MessageBoxWindowViewModel>().Setup(messagebox =>
            {
                messagebox.Content = container.Get<TViewModel>();
            })
            .ShowDialog<MessageBoxResults>();
    }
}
