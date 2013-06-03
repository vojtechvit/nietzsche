namespace NietzscheBiography.WebSite.Infrastructure
{
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Linq;


    public static class Util
    {
        public static string TextEnum(
            IEnumerable<object> values, 
            ListConjunction conjunction = ListConjunction.And,
            bool serialComma = false)
        {
            var sb = new StringBuilder();
            int i = 1;
            string previous = null;

            foreach (var value in values)
            {
                if (i == 1)
                {
                    sb.Append(value);
                }
                else
                {
                    if (previous != null)
                    {
                        sb.Append(", ");
                        sb.Append(previous);
                    }

                    previous = value.ToString();
                }

                i++;
            }

            if (previous != null)
            {
                if (serialComma)
                {
                    sb.Append(",");
                }

                switch (conjunction)
                {
                    case ListConjunction.And:
                        sb.Append(" and ");
                        break;
                    case ListConjunction.Or:
                        sb.Append(" or ");
                        break;
                    case ListConjunction.Nor:
                        sb.Append(" nor ");
                        break;
                }
                sb.Append(previous);
            }

            return sb.ToString();
        }
    }
}