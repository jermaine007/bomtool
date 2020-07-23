using Scriban;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NooneUI.Pdf.View
{
    class TemplateEngine
    {
        public string Render(string templatePath, object data)
        {
            var templateContent = File.ReadAllText(templatePath);
            var template = Template.Parse(templateContent);
            return template.Render(data);
        }

    }
}
