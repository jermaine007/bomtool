using Noone.UI.Models;
using Noone.UI.ViewModels;
using System;
using System.Threading.Tasks;

namespace Noone.UI
{
    public interface IMessageBox
    {
        Task<MessageBoxResults> ShowAsync(string message);

        Task<MessageBoxResults> ShowAsync(string title, string message);

        Task<MessageBoxResults> ShowAsync(string title, string message, MessageBoxSettingsViewModel settings);

        Task<MessageBoxResults> ShowCustomizeAsync(object customView, MessageBoxSettingsViewModel settings = null);

        Task<MessageBoxResults> ShowCustomizeAsync<TViewModel>(Action<TViewModel> setup = null, MessageBoxSettingsViewModel settings = null) where TViewModel : MessageBoxViewModel;
    }
}
