using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace NooneUI.Framework
{
    internal class LightDialog : IDialog
    {
        public async Task<string[]> OpenFileDialogAsync(string title, string filters, bool allowMultiple = false)
        {
            var dialog = new OpenFileDialog
            {
                Title = title,
                Filters = GetFilters(filters),
                AllowMultiple = allowMultiple
            };
            return await dialog.ShowAsync(LightApplicationBase.MainWindow);
        }

        public async Task<string> OpenFolderDialogAsync(string title, string initDirectory)
        {
            var dialog = new OpenFolderDialog
            {
                Title = title,
                Directory = initDirectory
            };
            return await dialog.ShowAsync(LightApplicationBase.MainWindow);
        }

        public async Task<string> SaveFileDialogAsync(string title, string initialFileName, string filters)
        {
            var dialog = new SaveFileDialog
            {
                Title = title,
                InitialFileName = initialFileName,
                Filters = GetFilters(filters)
            };
            return await dialog.ShowAsync(LightApplicationBase.MainWindow);
        }

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