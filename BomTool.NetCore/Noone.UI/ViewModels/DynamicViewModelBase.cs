using Noone.UI.Core;

namespace Noone.UI.ViewModels
{
    /// <summary>
    /// Dynamic view model base, should implement <see cref="IDynamicViewModel.Template"/> and <see cref="IDynamicViewModel.DataSource"/>
    /// </summary>
    [AutoRegister]
    public abstract class DynamicViewModelBase : ViewModelBase, IDynamicViewModel
    {
        /// <summary>
        /// <inheritdoc cref="IDynamicViewModel.Template"/>
        /// </summary>
        public abstract string Template { get; }

        /// <summary>
        /// <inheritdoc cref="IDynamicViewModel.DataSource"/>
        /// </summary>
        public abstract object DataSource { get; }

    }
}
