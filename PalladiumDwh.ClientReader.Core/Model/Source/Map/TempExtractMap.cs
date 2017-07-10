using System;
using CsvHelper.Configuration;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Model.Source.Map
{

    public class TempExtractMap
    {
        public static DefaultCsvClassMap<T> GetMap<T>()
        {
            var customerMap = new DefaultCsvClassMap<T>();
            foreach (var p in typeof(T).GetProperties())
            {
                var map = new CsvPropertyMap(p);
                map.Name(p.Name);

                if (p.PropertyType == typeof(string))
                {
                    map.TypeConverter<StringConverter>();
                }
                if (p.PropertyType == typeof(DateTime?))
                {
                    map.TypeConverter<NullDateConverter>();
                }
                if (p.PropertyType == typeof(DateTime))
                {
                    map.TypeConverter<DateConverter>();
                }

                if (p.PropertyType == typeof(int?))
                {
                    map.TypeConverter<NullIntConverter>();
                }
                if (p.PropertyType == typeof(int))
                {
                    map.TypeConverter<IntConverter>();
                }

                if (p.PropertyType == typeof(decimal?))
                {
                    map.TypeConverter<NullDecimalConverter>();
                }
                if (p.PropertyType == typeof(decimal))
                {
                    map.TypeConverter<DecimalConverter>();
                }
                customerMap.PropertyMaps.Add(map);
            }
            return customerMap;
        }
    }
}
