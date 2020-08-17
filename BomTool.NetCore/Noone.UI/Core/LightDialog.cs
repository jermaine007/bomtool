using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Noone.UI.Controls;

namespace Noone.UI.Core
{
    /// <summary>
    /// Default dialog service, provides open file, open folder and save file features.
    /// </summary>
    [AutoRegister(Singleton = true, InterfaceType = typeof(IDialog))]
    internal class LightDialog : IDialog
    {
        /// <summary>
        /// Open file dialog
        /// </summary>
        /// <param name="title">title</param>
        /// <param name="filters">filters</param>
        /// <param name="allowMultiple">allow multiple selection</param>
        /// <returns></returns>
        public async Task<string[]> OpenFileDialogAsync(string title, string filters, bool allowMultiple = false)
             => await new OpenFileDialog
             {
                 Title = title,
                 Filters = GetFilters(filters),
                 AllowMultiple = allowMultiple
             }.ShowAsync(LightApplicationBase.MainWindow);

        /// <summary>
        /// Open folder dialog
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="initDirectory">default directory at start up</param>
        /// <returns></returns>
        public async Task<string> OpenFolderDialogAsync(string title, string initDirectory)
            => await new OpenFolderDialog
            {
                Title = title,
                Directory = initDirectory
            }.ShowAsync(LightApplicationBase.MainWindow);


        /// <summary>
        /// Save file dialog
        /// </summary>
        /// <param name="title"></param>
        /// <param name="initialFileName"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<string> SaveFileDialogAsync(string title, string initialFileName, string filters)
            => await new SaveFileDialog
            {
                Title = title,
                InitialFileName = initialFileName,
                Filters = GetFilters(filters)
            }.ShowAsync(LightApplicationBase.MainWindow);


        /// <summary>
        /// Get filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        private List<FileDialogFilter> GetFilters(string filters)
        {
            if (string.IsNullOrEmpty(filters))
            {
                return new List<FileDialogFilter>{
                     new FileDialogFilter{
                         Name = "All files",
                         Extensions = new List<string> { "*" }
                    }};
            }

            return filters.Split(",").Select(f =>
            {
                var v = f.Split("|");
                return new FileDialogFilter
                {
                    Name = v[0],
                    Extensions = v[1].Split(";").ToList()
                };
            }).ToList();
        }

    }
}
