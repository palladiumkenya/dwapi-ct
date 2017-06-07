using CsvHelper.TypeConversion;

namespace PalladiumDwh.Shared.Custom
{
    public class NullDecimalConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            if (string.IsNullOrWhiteSpace(text) || (text.Trim().ToLower() == "NULL".ToLower()))
            {
                return default(decimal?);
            }
            decimal? d = null;
            decimal d2;
            bool success = decimal.TryParse(text, out d2);
            if (success) d = d2;
            return d;
        }
        public override bool CanConvertFrom(System.Type type)
        {
            return type == typeof(string);
        }
    }
}