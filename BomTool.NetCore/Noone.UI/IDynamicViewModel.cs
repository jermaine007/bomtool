namespace Noone.UI
{
    /// <summary>
    /// Interface provides the dynamic view view model
    /// </summary>
    public interface IDynamicViewModel : IViewModel
    {
        /// <summary>
        /// The dynamic view template path
        /// </summary>
        string Template { get; }

        /// <summary>
        /// The dynamic view data source, which is used to render view
        /// </summary>
        /// <value></value>
        object DataSource { get; }

    }
}
