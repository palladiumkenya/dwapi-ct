using System;
using CsvHelper.TypeConversion;

namespace PalladiumDwh.Shared.Custom
{
    public class NullIntConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            if (string.IsNullOrWhiteSpace(text) || (text.Trim().ToLower() == "NULL".ToLower()))
            {
                return default(int?);
            }
            int? d = null;
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