namespace PalladiumDwh.ClientReader.Core.Model
{
    public class ExtractFieldMap
    {
        public string Name { get; set; }
        public string Alias { get; set; }

        public ExtractFieldMap(string name, string @alias)
        {
            Name = name;
            Alias = alias;
        }

        public ExtractFieldMap(string name) : this(name, name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name.ToLower().Trim() == Alias.ToLower().Trim() ? $"{Name}" : $"{Name} AS {Alias}";
        }
    }
}