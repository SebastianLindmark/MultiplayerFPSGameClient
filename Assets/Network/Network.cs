using System.Net.Sockets;
using dto;
using Events.Parsers;
using UnityEngine;

namespace Network
{
    public class Network
    {
        private readonly NetworkServer networkServer;
        private readonly NetworkClient networkClient;

        private bool connected = false;
        
        public Network(PacketListener packetListener)
        {
            UdpClient udpClient = new UdpClient(0);
            networkClient = new NetworkClient(udpClient, "127.0.0.1", 59090);
            networkServer = new NetworkServer(udpClient, 50501, packetListener);
        }


        public void Start()
        {
           Connect();
        }

        private void Connect()
        {
            connected = networkClient.Connect();
            networkServer.StartReceive();
            if (!connected)
            {
                
            }
        }

        public void Send(Packet packet)
        {
            networkClient.Send(packet);
        }


        public void Stop()
        {
            networkClient.Disconnect();
            networkServer.Disconnect();
        }
    }
}