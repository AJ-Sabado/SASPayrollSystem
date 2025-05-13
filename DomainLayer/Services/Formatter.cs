using System.Globalization;

namespace DomainLayer.Services
{
    public class Formatter : IFormatter
    {
        public string ToProperCase(string input)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(input.Trim().ToLowerInvariant());
        }
    }
}
