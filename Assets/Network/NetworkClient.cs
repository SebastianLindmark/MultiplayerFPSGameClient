    using System;
    using System.Net.Sockets;
    using UnityEngine;

    namespace Network
    {
        public class NetworkClient
        {
        
            private readonly UdpClient udpClient;
            private readonly string ip;
            private readonly int port;
        
            //"127.0.0.1" 59090
            public NetworkClient(UdpClient udpClient, string ip, int port)
            {
                this.udpClient = udpClient;
                this.ip = ip;
                this.port = port;
            }

            public bool Connect()
            {
                try
                {
                    udpClient.Connect(ip, port);
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
                packet.AppendPacketSizeHeader();
                udpClient.Send(packet.getPayload(), packet.getPayload().Length);
            }

            public void Disconnect()
            {
                udpClient.Close();
            }


        }
    }