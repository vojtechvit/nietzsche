namespace NietzscheBiography.WebSite.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(ISentenceTemplateRepositoryContract))]
    public interface ISentenceTemplateRepository
    {
        string Find(string templateName, IEnumerable<string> thematicRoles);
    }

    [ContractClassFor(typeof(ISentenceTemplateRepository))]
    internal abstract class ISentenceTemplateRepositoryContract : ISentenceTemplateRepository
    {
        public string Find(string templateName, IEnumerable<string> thematicRoles)
        {
            Contract.Requires<ArgumentNullException>(templateName != null);
            Contract.Requires<ArgumentNullException>(thematicRoles != null);

            return null;
        }
    }
}
