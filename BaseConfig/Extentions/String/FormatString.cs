using System.Text.RegularExpressions;

namespace BaseConfig.Extentions.String
{
    public static class FormatString
    {
        public static string WithRegex(string text)
        {
            return Regex.Replace(text, @"\s+", " ");
        }
    }
}
