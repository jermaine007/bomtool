using Noone.UI.Models;
using Noone.UI.ViewModels;
using System.Threading.Tasks;

namespace Noone.UI
{
    public interface IMessageBox
    {
        Task<MessageBoxResults> ShowAsync(string message);

        Task<MessageBoxResults> ShowAsync(string title, string message);

        Task<MessageBoxResults> ShowAsync(string title, string message, MessageBoxSettingsViewModel settings);

        Task<MessageBoxResults> ShowCustomizeAsync(object customView);

        Task<MessageBoxResults> ShowCustomizeAsync<TViewModel>() where TViewModel : IViewModel;
    }
}
