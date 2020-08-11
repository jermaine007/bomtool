namespace NooneUI.Framework
{
    /// <summary>
    /// Indicate that the class which implement this interface has Id
    /// </summary>
    public interface IHaveId
    {
        string Id => ContainerLocator.Current.Get<IdGenerator>().Next(this.GetType().FullName,"{0}@{1}");
    }
}
