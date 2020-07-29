namespace NooneUI.Framework
{
    public class Presenter
    {
        public static Presenter Current {get; private set;}

        static Presenter() 
        {
            Current = new Presenter();
        }

        
    }
}