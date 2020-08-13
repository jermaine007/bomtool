namespace NooneUI.Framework
{
    public interface IContainerProvider
    {
        IContainer Container => ContainerLocator.Current;
    }
}
