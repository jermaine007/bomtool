namespace Noone.UI
{
    public interface ITemplateEngine
    {
        string Render(string templateFile, object data);
    }
}
