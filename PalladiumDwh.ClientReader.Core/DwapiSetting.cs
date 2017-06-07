namespace PalladiumDwh.ClientReader.Core
{
    public class DwapiSetting
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Category { get; set; }

        public DwapiSetting(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public DwapiSetting(string name, string value, string category) : this(name, value)
        {
            Category = category;
        }
    }
}