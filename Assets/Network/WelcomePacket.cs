using System;
using util;

namespace Network
{
    public class WelcomePacket : Packet
    {
        private int packetSize;

        public int PacketSize => packetSize;

        public byte[] Payload => payload;

        private byte[] payload;


        public WelcomePacket(int packetSize, byte[] payload)
        {
            this.packetSize = packetSize;
            this.payload = payload;
        }

        public WelcomePacket(byte[] payload)
        {
            packetSize = payload.Length;
            this.payload = payload;
        }

        public void AppendPacketSizeHeader()
        {
            byte[] sizeByteArr = BitConverter.GetBytes(packetSize);
            byte[] destination = new byte[sizeByteArr.Length + packetSize];
            Util.CopyBytes(destination,sizeByteArr,payload);
            payload = destination;

        }

        public byte[] getPayload()
        {
            return payload;
        }
    }
}