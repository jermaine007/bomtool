namespace NooneUI.Framework
{
    public interface IHaveId
    {
        string Id => ContainerLocator.Current.Get<IdGenerator>().Next(this.GetType());
    }
}
