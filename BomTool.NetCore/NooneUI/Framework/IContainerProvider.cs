namespace NooneUI.Framework
{
    public interface IContainerProvider
    {
        LightContainer Container => LightContainer.Current;
    }
}