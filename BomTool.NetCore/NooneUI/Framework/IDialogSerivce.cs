using System.Threading.Tasks;

namespace NooneUI.Framework
{
    public interface IDialogSerivce
    {
        Task<string[]> OpenFileDialogAsync(string title = null,
                                string filters = null,
                                bool allowMultiple = false);

        Task<string> SaveFileDialogAsync(string title = null, string initialFileName = null, string filters = null);

        Task<string> OpenFolderDialogAsync(string titile = null, string initDirectory = null);
    }
}