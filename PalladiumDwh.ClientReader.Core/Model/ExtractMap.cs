using System.Collections.Generic;
using System.Text;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class ExtractMap
    {
        public string Name { get; set; }
        public List<ExtractFieldMap> FieldMaps { get; set; } = new List<ExtractFieldMap>();

        public string GetSqlStatement()
        {
            string sql = string.Empty;

            sql = $"SELECT {0} FROM {Name}";

            return sql;
        }

        public string BuildFields()
        {
            StringBuilder sc = new StringBuilder();

            foreach (var fieldMap in FieldMaps)
            {
                sc.AppendLine($"{fieldMap},");
            }
            var result = sc.ToString();

            return result.Length > 1 ? result.Remove(result.Length - 1) : result;
        }
    }
}