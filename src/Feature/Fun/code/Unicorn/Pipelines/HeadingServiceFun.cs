using System;
using System.IO;
using System.Reflection;
using Unicorn.ControlPanel.Headings;

namespace Helixbase.Feature.Fun.Unicorn.Pipelines
{
    /// <summary>
    /// https://github.com/SitecoreUnicorn/Unicorn/blob/master/src/Unicorn/ControlPanel/Headings/HeadingService.cs
    /// </summary>
    public class HeadingServiceFun : HeadingService
    {
        private static readonly Random Random = new Random();

        private static readonly string[] HtmlChoices =
        {
            "Helixbase.Feature.Fun.Unicorn.Images.Helixbase.html",
            "Helixbase.Feature.Fun.Unicorn.Images.Unicorn.html",
            "Helixbase.Feature.Fun.Unicorn.Images.Unicorn.svg.html",
            "Helixbase.Feature.Fun.Unicorn.Images.Unicorn2.svg.html",
            "Helixbase.Feature.Fun.Unicorn.Images.Unicorn3.svg.html"
        };

        public new string GetHeadingHtml()
        {
            // heh heh :)
            //if (DateTime.Today.Month == 4 && DateTime.Today.Day == 1) return ReadResource("Unicorn.ControlPanel.Headings.April.svg.html");

            var headerIndex = Random.Next(0, HtmlChoices.Length);

            return ReadResource(HtmlChoices[headerIndex]);
        }

        protected override string ReadResource(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(name))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}