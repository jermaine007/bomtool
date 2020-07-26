namespace BomTool.NetCore.Framework
{
    public interface IContainerProvider
    {
        Container Container => Container.Current;
    }
}