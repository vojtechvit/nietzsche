namespace NietzscheBiography.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public partial class Name
    {
        public override string ToString()
        {
            return string.Join(" ", this.Prefix, this.GivenNames, this.LastName, this.Suffix);
        }
    }
}
