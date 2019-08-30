namespace PalladiumDwh.Core.Exchange
{
    public class StatDto
    {
        public string Name { get; set; }
        public int Recieved { get; set; }

        public StatDto(string name, int recieved)
        {
            Name = name;
            Recieved = recieved;
        }
    }
}
