namespace NooneUI.Framework
{
    public interface IDynamicViewPresenter
    {
         IView GetView(IDynamicViewModel vm);
    }
}
