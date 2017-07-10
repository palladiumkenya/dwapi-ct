using CsvHelper.TypeConversion;
using PalladiumDwh.Shared.Extentions;

namespace PalladiumDwh.Shared.Custom
{
    public class StringConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            if (string.IsNullOrWhiteSpace(text) || (text.Trim().ToLower() == "NULL".ToLower()))
            {
                return default(string);
            }
            return text.Truncate(150);
        }
        public override bool CanConvertFrom(System.Type type)
        {
            return type == typeof(string);
        }
    }
}