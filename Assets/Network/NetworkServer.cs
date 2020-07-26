using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace Network
{
    public class NetworkServer
    {
        private UdpClient udpClient;
        private IPEndPoint ipEndPoint;
        private readonly PacketListener packetListener;

        private bool listening = true;
        private readonly Thread receiveThread;

        //"127.0.0.1" 59090
        public NetworkServer(UdpClient udpClient, IPEndPoint ipEndPoint, PacketListener packetListener)
        {
            this.udpClient = udpClient;
            this.ipEndPoint = ipEndPoint;
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
                try
                {
                    Debug.Log("Listening for packets");
                    byte[] received = udpClient.Receive(ref ipEndPoint);
                    SplitPackets(received);

                }
                catch (SocketException e)
                {
                    if (e.ErrorCode == 10054)
                    {
                        Debug.Log("Remote port closed, retrying...");
                    }

                    throw e;
                }
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
            Packet packet = ConstructPacket(packetSize, payloadOffset, payload);
            packetListener.onReceive(packet);
        }

        private Packet ConstructPacket(int packetSize, int payloadOffset, byte[] payload)
        {
            byte[] packetPayload = new byte[packetSize];
            Array.Copy(payload, payloadOffset, packetPayload, 0, packetSize);
            return new Packet(packetSize, packetPayload);
        }


        public void Disconnect()
        {
            listening = false;
            receiveThread.Abort();
            udpClient.Close();
        }
    }
}