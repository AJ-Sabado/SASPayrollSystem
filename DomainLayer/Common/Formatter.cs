using System.Globalization;

namespace DomainLayer.Common
{
    public class Formatter : IFormatter
    {
        private TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        public string ToProperCase(string input)
        {
            return textInfo.ToTitleCase(input.Trim().ToLowerInvariant());
        }
    }
}
