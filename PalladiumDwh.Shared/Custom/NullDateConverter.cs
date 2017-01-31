using System;
using System.ComponentModel;
using CsvHelper.TypeConversion;

namespace PalladiumDwh.Shared.Custom
{
    public class NullDateConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            if (string.IsNullOrWhiteSpace(text) || (text.Trim().ToLower() == "NULL".ToLower()))
            {
                return default(DateTime?);
            }
            DateTime? d = null;
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