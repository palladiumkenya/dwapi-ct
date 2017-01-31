using CsvHelper.TypeConversion;

namespace PalladiumDwh.Shared.Custom
{
    public class IntConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            if (string.IsNullOrWhiteSpace(text) || (text.Trim().ToLower() == "NULL".ToLower()))
            {
                return -99;
            }
            int d = -99;
            int d2;
            bool success = int.TryParse(text, out d2);
            if (success) d = d2;
            return d;
        }
        public override bool CanConvertFrom(System.Type type)
        {
            return type == typeof(string);
        }
    }
}