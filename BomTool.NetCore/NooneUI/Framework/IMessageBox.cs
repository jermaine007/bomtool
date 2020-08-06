using System.Threading.Tasks;

namespace NooneUI.Framework
{
    public interface IMessageBox
    {
         Task<MessageBoxResults> ShowAsync(string message);

         Task<MessageBoxResults> ShowAsync(string title, string message);

         Task<MessageBoxResults> ShowAsync(string tile,string message, MessageBoxSettings settings);
    }
}
