namespace NooneUI.Framework
{
    [AutoRegister]
    public abstract class DynamicViewModelBase : ViewModelBase, IDynamicViewModel
    {
        public abstract string Template { get; }
        public abstract object DataSource { get; }

    }
}
