namespace Network
{
    public interface Packet
    {
        void AppendPacketSizeHeader();
        byte[] getPayload();
    }
}