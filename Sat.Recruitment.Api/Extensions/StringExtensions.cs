using System;

namespace Sat.Recruitment.Api.Extensions
{
    public static class StringExtensions
    {
        public static string NormalizeEmail(this string text) 
        {
            var splitedEmail = text.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = splitedEmail[0].IndexOf("+", StringComparison.Ordinal);

            splitedEmail[0] = atIndex < 0 ? splitedEmail[0].Replace(".", "") : splitedEmail[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { splitedEmail[0], splitedEmail[1] });
        }
    }
}
