using System;
using CsvHelper.TypeConversion;

namespace PalladiumDwh.Shared.Custom
{
    public class NullStringConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            if (string.IsNullOrWhiteSpace(text) || (text.Trim().ToLower() == "NULL".ToLower()))
            {
                return default(string);
            }
            return text;
        }
        public override bool CanConvertFrom(System.Type type)
        {
            return type == typeof(string);
        }
    }
}