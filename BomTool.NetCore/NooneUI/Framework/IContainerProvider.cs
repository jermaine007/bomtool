namespace NooneUI.Framework
{
    public interface IContainerProvider
    {
        Container Container => Container.Current;
    }
}