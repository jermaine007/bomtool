namespace NooneUI.Framework
{
    public interface ITemplateEngine
    {
        string Render(string templateFile, object data);
    }
}
