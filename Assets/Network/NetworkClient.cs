    using System.Net.Sockets;

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

            public void Connect()
            {
                udpClient.Connect(ip, port);
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