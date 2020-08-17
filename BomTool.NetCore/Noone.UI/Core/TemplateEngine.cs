using System.IO;
using Scriban;

namespace Noone.UI.Core
{
    [AutoRegister(Singleton = true, InterfaceType = typeof(ITemplateEngine))]
    internal class TemplateEngine : ITemplateEngine
    {
        public string Render(string templateFile, object data)
        {

            string content = File.ReadAllText(templateFile);
            if (data != null)
            {
                Template template = Template.Parse(content);
                return template.Render(data);
            }
            return content;
        }
    }
}
