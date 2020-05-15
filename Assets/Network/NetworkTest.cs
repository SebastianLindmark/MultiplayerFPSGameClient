using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Network
{
    public class NetworkTest
    {

        static UdpClient udpClient; // = new UdpClient(50501);

        //public static bool running = true;



        //[RuntimeInitializeOnLoadMethod]
        static void OnRuntimeMethodLoad()
        {

            //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
            //IPAddress ipAdd = System.Net.IPAddress.Parse("127.0.0.1");
            //IPEndPoint remoteEP = new IPEndPoint(ipAdd, 59090);





            try
            {
                udpClient.Connect("127.0.0.1", 59090);

                byte[] initalRequestBytes = constructInitalRequest();
                byte[] positionActionBytes = constructPositionAction();


                udpClient.Send(initalRequestBytes, initalRequestBytes.Length);
                Thread.Sleep(100);
                udpClient.Send(positionActionBytes, positionActionBytes.Length);

                udpClient.BeginReceive(new AsyncCallback(recv), null); //can be used with async keyword


            }
            catch (Exception e)
            {
                Debug.LogWarning(e.StackTrace);
            }



            Thread receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.IsBackground = true;
            receiveThread.Start();

            Thread sendThread = new Thread(new ThreadStart(sendPositionalData));
            sendThread.IsBackground = true;
            sendThread.Start();


        }

        static void sendPositionalData() {


            while (true) {

                Thread.Sleep(1500);
                Debug.Log("Sending position data");
                byte[] positionData = constructPositionAction();
                udpClient.Send(positionData, positionData.Length);

            }


        }


        static byte[] constructInitalRequest()
        {
            byte[] modeByte = { 0 };

            Debug.Log("Mode byte length is " + modeByte.Length);
            System.Random r = new System.Random();
            int playerId = r.Next(1000, 9999);
            byte[] playerIdBytes = BitConverter.GetBytes(playerId);

            Debug.Log("Player id is " + playerId);


            int reservedPayloadBytes = 4;

            int sendSize = modeByte.Length + playerIdBytes.Length;


            byte[] sendSizeBytes = BitConverter.GetBytes(sendSize);


            List<byte[]> byteArrays = new List<byte[]>();
            byteArrays.Add(sendSizeBytes);
            byteArrays.Add(modeByte);
            byteArrays.Add(playerIdBytes);

            byte[] combined = new byte[sendSize + reservedPayloadBytes];

            int byteCounter = 0;
            for (int i = 0; i < byteArrays.Count; i++)
            {
                Debug.Log(byteCounter + " : " + byteArrays[i]);
                byteArrays[i].CopyTo(combined, byteCounter);
                byteCounter += byteArrays[i].Length;
            }

            return combined;

        }
    

    
        private static byte[] constructPositionAction() {
            byte[] modeByte = { 10 };

            //Debug.Log("Mode byte length is " + modeByte.Length);
            System.Random random = new System.Random();
            byte[] positionXBytes = BitConverter.GetBytes(-100 + 0.1f);
            byte[] positionYBytes = BitConverter.GetBytes(100 + 0.2f);
            byte[] positionZBytes = BitConverter.GetBytes(100 + 0.3f);
            byte[] rotationBytes = BitConverter.GetBytes(100 + 0.4f);


        
        

            int reservedPayloadBytes = 4;

            int sendSize = modeByte.Length + positionXBytes.Length + positionYBytes.Length + positionZBytes.Length + rotationBytes.Length;


            byte[] sendSizeBytes = BitConverter.GetBytes(sendSize);

            List<byte[]> byteArrays = new List<byte[]>();
            byteArrays.Add(sendSizeBytes);
            byteArrays.Add(modeByte);
            byteArrays.Add(positionXBytes);
            byteArrays.Add(positionYBytes);
            byteArrays.Add(positionZBytes);
            byteArrays.Add(rotationBytes);



            byte[] combined = new byte[sendSize + reservedPayloadBytes];

            int byteCounter = 0;
            for (int i = 0; i < byteArrays.Count; i++)
            {
                //Debug.Log(byteCounter + " : " + byteArrays[i]);
                byteArrays[i].CopyTo(combined, byteCounter);
                byteCounter += byteArrays[i].Length;
            }



            return combined;

        }


        private static void parsePacket(byte[] packetData, int packetSize) { 
    

    
        }

        private static int parsePositionPacket(byte[] packetData, int startIndex) {

            float playerIdentifier = BitConverter.ToSingle(packetData, startIndex);
            float positionX = BitConverter.ToSingle(packetData, startIndex + 4);
            float positionY = BitConverter.ToSingle(packetData, startIndex + 8);
            float positionZ = BitConverter.ToSingle(packetData, startIndex + 12);
            float rotation = BitConverter.ToSingle(packetData, startIndex + 16);
            Debug.Log("Position x " + positionX);

            return startIndex + 20;
        }

        private static bool running = true;


        private static void ReceiveData() {
            while (running) {

                try
                {
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 50501);
                    byte[] received = udpClient.Receive(ref RemoteIpEndPoint);

                    int currentIndex = 0;
                    while (currentIndex < received.Length) {
                        int packetSize = BitConverter.ToInt32(received, currentIndex);
                        currentIndex = parsePositionPacket(received, currentIndex + 4);
                    }




                    /*Debug.Log("Received " + received.Length + " bytes");
                byte[] playerIdArray = new byte[4];

                Array.Copy(received, playerIdArray, 4);

                byte[] playerPosX = new byte[4];
                //Array.Copy(received, 4,playerPosX, 0 , 4);


                int playerIdentifier = BitConverter.ToInt32(playerIdArray, 0);
                int playerPos = BitConverter.ToInt32(playerPosX, 0);
                byte[] convertedPlayerPos = BitConverter.GetBytes(playerPos);

                Debug.Log("Received player position x " + playerPos);*/

                }
                catch (Exception err) {
                    Debug.Log(err);
                }

            }


        }


        private static void recv(IAsyncResult res)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 50501);
            byte[] received = udpClient.EndReceive(res, ref RemoteIpEndPoint);
            udpClient.BeginReceive(new AsyncCallback(recv), null);

            //Process codes
            Debug.Log("Received message from server");
            Debug.Log("Size of message " + received.Length);


            Debug.Log(Encoding.ASCII.GetString(received));        
        }



        [RuntimeInitializeOnLoadMethod]
        static void OnSecondRuntimeMethodLoad()
        {
            Debug.Log("SecondMethod After Scene is loaded and game is running.");
        }
    }
}
