namespace NietzscheBiography.WebSite.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    public class SentenceSynthesizer : ISentenceSynthesizer
    {
        private ISentenceTemplateRepository templateRepository;

        public SentenceSynthesizer(ISentenceTemplateRepository templateRepository)
        {
            Contract.Requires<ArgumentNullException>(templateRepository != null);

            this.templateRepository = templateRepository;
        }

        public string Synthetize(string templateName, IEnumerable<NounPhrase> nounPhrases)
        {
            var thematicRoles = from n in nounPhrases select n.ThematicRole;

            string template = this.templateRepository.Find(
                templateName,
                thematicRoles);

            if (template == null)
            {
                string errorMessageFormat = "No template was found for name \"{0}\" and thematic roles \"{1}\"";
                string errorMessage = string.Format(errorMessageFormat, template, String.Join(" ,", thematicRoles));

                throw new Exception(errorMessage);
            }

            return Populate(template, nounPhrases);
        }

        private static string Populate(string template, IEnumerable<NounPhrase> nounPhrases)
        {
            Contract.Requires<ArgumentNullException>(template != null);
            Contract.Requires<ArgumentNullException>(nounPhrases != null);

            string result = template;

            foreach (var group in nounPhrases.GroupBy(np => np.ThematicRole))
            {
                string thematicRole = group.Key;

                string textEnum = Util.TextEnum(group.Select(np => np.Text));
                result = result.Replace("{" + thematicRole + "}", textEnum);
            }

            return result;
        }

        
    }
}