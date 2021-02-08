using System;

namespace PalladiumDwh.Core.Exchange
{
    public class HandshakeDto
    {
        public Guid Id { get; set; }
        public Guid? Session { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Tag { get; set; }

        public HandshakeDto()
        {
        }

        public HandshakeDto(Guid id, DateTime end)
        {
            Id = id;
            End = end;
        }
    }
}
