using System;
using System.Net.Sockets;
using UnityEngine;

namespace Network
{
    public class NetworkClient
    {
        private readonly UdpClient udpClient;

        public NetworkClient(UdpClient udpClient)
        {
            this.udpClient = udpClient;
        }

        public void Send(Packet packet)
        {
            if (udpClient.Client != null)
            {
                packet.AppendPacketSizeHeader();
                udpClient.Send(packet.getPayload(), packet.getPayload().Length);
            }
        }

        public void Disconnect()
        {
            udpClient.Close();
        }
    }
}