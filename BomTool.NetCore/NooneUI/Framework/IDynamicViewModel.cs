namespace NooneUI.Framework
{
    public interface IDynamicViewModel : IViewModel
    {
        string Template { get; }
        object DataSource { get; }

    }
}
