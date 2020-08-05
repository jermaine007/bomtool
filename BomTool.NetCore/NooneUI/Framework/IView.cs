using Avalonia;

namespace NooneUI.Framework
{
    public interface IView
    {
        string Id { get; }

        object DataContext
        {
            get => (this as StyledElement)?.DataContext;
            set
            {
                if (this is StyledElement element)
                {
                    element.DataContext = value;
                }
            }
        }
    }
}