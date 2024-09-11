using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Services.Tools
{
    public class StringTools
    {
        public static string ToPascalCase(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string[] words = str.Split(new[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Concat(words.Select(word => textInfo.ToTitleCase(word.ToLower())));
        }
        public static string ToCapitalizedCase(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string[] words = str.Split(new[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", words.Select(word => textInfo.ToTitleCase(word.ToLower())));
        }

        public static string ToUppercase(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            return str.ToUpper();
        }
    }
}
