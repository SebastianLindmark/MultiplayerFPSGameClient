namespace Events.Parsers
{
    public interface PacketParser
    {

        GameEvent Parse(byte[] packetData, int startIndex);

    }
}