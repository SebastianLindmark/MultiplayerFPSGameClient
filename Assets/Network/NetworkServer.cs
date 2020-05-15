using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Network
{
    public class NetworkServer
    {
        private readonly UdpClient udpClient;
        private readonly int port;
        private readonly PacketListener packetListener;

        private bool listening = true;
        private readonly Thread receiveThread;

        //"127.0.0.1" 59090
        public NetworkServer(UdpClient udpClient, int port, PacketListener packetListener)
        {
            this.udpClient = udpClient;
            this.port = port;
            this.packetListener = packetListener;
            this.receiveThread = new Thread(Receive);
        }

        public void StartReceive()
        {
            listening = true;
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        private void Receive()
        {
            while (listening)
            {
                IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, port);
                byte[] received = udpClient.Receive(ref remoteIpEndPoint);
                SplitPackets(received);
            }
        }

        private void SplitPackets(byte[] payload)
        {
            int payloadSize = payload.Length;

            int index = 0;
            while (index < payloadSize)
            {
                int packetSize = BitConverter.ToInt32(payload, 0);
                index += 4; // move down
                NotifyPacket(packetSize, index, payload);
                index += packetSize;
            }
        }

        private void NotifyPacket(int packetSize, int payloadOffset, byte[] payload)
        {
            GamePacket packet = ConstructPacket(packetSize, payloadOffset, payload);
            packetListener.onReceive(packet);
        }

        private GamePacket ConstructPacket(int packetSize, int payloadOffset, byte[] payload)
        {
            byte[] packetPayload = new byte[packetSize];
            Array.Copy(payload, payloadOffset, packetPayload, 0, packetSize);
            return new GamePacket(packetSize, packetPayload);
        }


        public void Disconnect()
        {
            listening = false;
            receiveThread.Abort();
            udpClient.Close();
        }
    }
}