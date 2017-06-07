using CsvHelper.TypeConversion;

namespace PalladiumDwh.Shared.Custom
{
    public class DecimalConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            if (string.IsNullOrWhiteSpace(text) || (text.Trim().ToLower() == "NULL".ToLower()))
            {
                return new decimal(-19.99);
            }
            decimal d = new decimal(-19.99);
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