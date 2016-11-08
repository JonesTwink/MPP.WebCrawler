using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerUI.Config
{
    public class PreferencesLoader
    {
        public int Depth { get; set; } = 1;
        public List<string> URLs { get; set; } = new List<string>();

        internal void LoadPreferences()
        {
            try
            {
                Depth = Int32.Parse(ConfigurationManager.AppSettings["CrawlerDepth"]);
            }
            catch(FormatException)
            {
                throw new PreferencesException("Check `CrawlerDepth` in app.config file.");
            }

            URLs = GetURLSection();
            if (URLs.Count == 0)
            {
                throw new PreferencesException("List of URLs is empty. Check `URLSection` in app.config file.");
            }

        }

        private List<string> GetURLSection()
        {
            List<string> URLStringsList = new List<string>();
            URLConfigSection URLSection = (URLConfigSection)ConfigurationManager.GetSection("URLSection");

            foreach (URLElement URLElement in URLSection.URLs)
            {
                URLStringsList.Add(URLElement.url);
            }

            return URLStringsList;
        }

    }
}
