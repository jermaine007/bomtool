using Noone.UI.Models;
using Noone.UI.ViewModels;
using System;
using System.Threading.Tasks;

namespace Noone.UI.Core
{
    [AutoRegister(Singleton = true, InterfaceType = typeof(IMessageBox))]
    internal class LightMessageBox : IMessageBox, IContainerProvider
    {
        private readonly IContainer container;

        public LightMessageBox() => container = ((IContainerProvider)this).Container;

        public Task<MessageBoxResults> ShowAsync(string message) =>
            container.Get<MessageBoxWindowViewModel>().Setup(messagebox =>
            {
                messagebox.Settings = container.Get<MessageBoxSettingsViewModel>().Setup(settings =>
                {
                    settings.Message = message;
                    settings.Buttons = MessageBoxButtons.OK;
                });
                messagebox.Content = container.Get<MessageBoxViewModel>();
            })
            .ShowDialog<MessageBoxResults>();


        public Task<MessageBoxResults> ShowAsync(string title, string message) =>
            container.Get<MessageBoxWindowViewModel>().Setup(messagebox =>
            {
                messagebox.Settings = container.Get<MessageBoxSettingsViewModel>().Setup(settings =>
                {
                    settings.Message = message;
                    settings.Title = title;
                    settings.Buttons = MessageBoxButtons.OK;
                });
                messagebox.Content = container.Get<MessageBoxViewModel>();

            })
            .ShowDialog<MessageBoxResults>();

        public Task<MessageBoxResults> ShowAsync(string title, string message, MessageBoxSettingsViewModel settings) =>
            container.Get<MessageBoxWindowViewModel>().Setup(messagebox =>
            {
                settings.Title = title;
                settings.Message = message;
                messagebox.Settings = settings;
                messagebox.Content = container.Get<MessageBoxViewModel>();

            })
            .ShowDialog<MessageBoxResults>();

        public Task<MessageBoxResults> ShowCustomizeAsync(object customView, MessageBoxSettingsViewModel settings = null) =>
            container.Get<MessageBoxWindowViewModel>().Setup(messagebox =>
            {
                messagebox.Settings = settings ?? container.Get<MessageBoxSettingsViewModel>();
                messagebox.Content = customView;
            })
            .ShowDialog<MessageBoxResults>();


        public Task<MessageBoxResults> ShowCustomizeAsync<TViewModel>(Action<TViewModel> setup, MessageBoxSettingsViewModel settings = null)
            where TViewModel : MessageBoxViewModel
                => container.Get<MessageBoxWindowViewModel>().Setup(messagebox =>
                {
                    TViewModel vm = container.Get<TViewModel>();
                    setup?.Invoke(vm);
                    messagebox.Settings = settings ?? container.Get<MessageBoxSettingsViewModel>();
                    messagebox.Content = vm;
                })
                .ShowDialog<MessageBoxResults>();
    }
}
