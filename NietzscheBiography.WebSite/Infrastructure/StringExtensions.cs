using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NietzscheBiography.WebSite.Infrastructure
{
    public static class StringExtensions
    {
        public static string Capitalize(this string str)
        {
            return str[0].ToString().ToUpper() + str.Substring(1);
        }
    }
}