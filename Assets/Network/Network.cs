using System.Net.Sockets;
using dto;
using Events.Parsers;
using UnityEngine;

namespace Network
{
    public class Network : PacketListener
    {
    
        private readonly NetworkServer networkServer;
        private readonly NetworkClient networkClient;

        public Network()
        {
            UdpClient udpClient = new UdpClient(50501);
            networkClient = new NetworkClient(udpClient, "127.0.0.1", 59090);
            networkServer = new NetworkServer(udpClient, 50501, this);
        }
    
    
        public void Start()
        {
            networkClient.Connect();
            networkServer.StartReceive();
        }
        
        public void Send(Packet packet)
        {
            networkClient.Send(packet);
        }

        public void onReceive(Packet packet)
        {
        
        }

        public void Stop()
        {
            networkClient.Disconnect();
            networkServer.Disconnect();
        }
    }
}