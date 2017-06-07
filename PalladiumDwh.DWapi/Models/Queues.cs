namespace PalladiumDwh.DWapi.Models
{
    public class Queues
    {
        public string Name { get; set; }
        public int Messages { get; set; }
        public int JournalMessages { get; set; }

        public Queues(string name, int messages, int journalMessages)
        {
            Name = name;
            Messages = messages;
            JournalMessages = journalMessages;
        }

       
    }
}