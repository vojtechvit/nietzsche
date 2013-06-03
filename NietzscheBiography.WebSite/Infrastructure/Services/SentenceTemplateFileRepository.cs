namespace NietzscheBiography.WebSite.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    public sealed class SentenceTemplateFileRepository : ISentenceTemplateRepository
    {
        public const string DefaultCacheEntryKey = "SentenceTemplates";

        private string templatesFilePath;
        private ObjectCache cache;
        private string cacheEntrykey;

        public SentenceTemplateFileRepository(
            string templatesFilePath,
            ObjectCache objectCache = null,
            string cacheEntryKey = null)
        {
            Contract.Requires<ArgumentNullException>(templatesFilePath != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(templatesFilePath));

            this.templatesFilePath = templatesFilePath;

            this.cache = objectCache ?? MemoryCache.Default;
            this.cacheEntrykey = cacheEntrykey ?? DefaultCacheEntryKey;
        }

        /// <summary>
        /// A memory storage for event title templates. The key is composed by following
        /// rule: {template}|{commaSeparatedThematicRolesInAscendingOrder}.
        /// </summary>
        private Dictionary<string, string> Templates
        {
            get
            {
                var templates = this.cache[this.cacheEntrykey] as Dictionary<string, string>;

                if (templates == null)
                {
                    templates = LoadFromFile(this.templatesFilePath);

                    var policy = new CacheItemPolicy();
                    policy.ChangeMonitors.Add(new HostFileChangeMonitor(new List<string> { this.templatesFilePath }));

                    this.cache.Set(this.cacheEntrykey, templates, policy);
                }

                return templates;
            }
        }

        private static Dictionary<string, string> LoadFromFile(string templatesFilePath)
        {
            var templates = new Dictionary<string, string>();

            var templatesDoc = XDocument.Load(templatesFilePath).Root;
            XNamespace ett = "urn:ndb:ett";

            foreach (var templateElement in templatesDoc.Elements(ett + "template"))
            {
                string templateName = templateElement.Attribute("name").Value;

                foreach (var versionElement in templateElement.Elements(ett + "version"))
                {
                    string templateVersion = versionElement.Value;
                    string optimizedTemplate = templateVersion;

                    var identifyingSet = new SortedSet<string>();
                    
                    foreach (Match match in Regex.Matches(templateVersion, @"\{[^}]+\}"))
                    {
                        string variableName = match.Value.Trim('{', '}');

                        optimizedTemplate = optimizedTemplate.Replace("{" + variableName + "}", "{" + variableName + "}");

                        identifyingSet.Add(variableName);
                    }

                    string key = templateName + "|" + string.Join(",", identifyingSet);

                    templates.Add(key, optimizedTemplate);
                }
            }

            return templates;
        }

        public string Find(string templateName, IEnumerable<string> thematicRoles)
        {
            var identifyingSet = new SortedSet<string>(thematicRoles);

            string key = templateName + "|" + string.Join(",", identifyingSet);

            return this.Templates[key];
        }
    }
}