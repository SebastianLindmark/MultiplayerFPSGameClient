using System;
using System.Net;
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
        private readonly UdpClient udpClient;
        private bool connected = false;

        private IPEndPoint remoteIpEndPoint;
        
        private string SERVER_IP = "127.0.0.1";
        private int SERVER_PORT = 59090;

        private int RECEIVE_PORT = 50501;
        
        public Network(PacketListener packetListener)
        {
            remoteIpEndPoint = new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT); // endpoint where server is listening
            
            udpClient = new UdpClient();
            networkClient = new NetworkClient(udpClient);
            networkServer = new NetworkServer(udpClient, remoteIpEndPoint, packetListener); 
            udpClient.Connect(remoteIpEndPoint);
        }
        
        public bool Connect()
        {
            try
            {
                networkServer.StartReceive();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
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