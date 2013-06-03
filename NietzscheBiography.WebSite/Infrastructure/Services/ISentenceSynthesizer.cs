namespace NietzscheBiography.WebSite.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(ISentenceSynthesizerContract))]
    public interface ISentenceSynthesizer
    {
        string Synthetize(string templateName, IEnumerable<NounPhrase> nounPhrases);
    }

    [ContractClassFor(typeof(ISentenceSynthesizer))]
    internal abstract class ISentenceSynthesizerContract : ISentenceSynthesizer
    {
        public string Synthetize(string templateName, IEnumerable<NounPhrase> nounPhrases)
        {
            Contract.Requires<ArgumentNullException>(templateName != null);
            Contract.Requires<ArgumentNullException>(nounPhrases != null);
            Contract.Ensures(Contract.Result<string>() != null);

            return null;
        }
    }
}