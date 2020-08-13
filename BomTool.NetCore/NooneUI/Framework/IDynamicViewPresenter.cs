namespace NooneUI.Framework
{
    public interface IDynamicViewPresenter
    {
        /// <summary>
        /// According to Dynamic view model to instantiate and bind the dynamic view
        /// Which would be usd in <see cref="ViewLocator"/>
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>Dynamic view instance</returns>
        IView GetView(IDynamicViewModel vm);
    }
}
