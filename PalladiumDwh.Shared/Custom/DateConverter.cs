using System;
using CsvHelper.TypeConversion;

namespace PalladiumDwh.Shared.Custom
{
    public class DateConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            if (string.IsNullOrWhiteSpace(text) || (text.Trim().ToLower() == "NULL".ToLower()))
            {
                return new DateTime(1900, 1, 1);
            }
            DateTime d =new DateTime(1900, 1, 1);
            DateTime d2;
            bool success = DateTime.TryParse(text, out d2);
            if (success) d = d2;
            return d;
        }
        public override bool CanConvertFrom(System.Type type)
        {
            return type == typeof(string);
        }
    }
}