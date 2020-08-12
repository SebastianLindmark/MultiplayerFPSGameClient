using System;
using System.Collections.Generic;
using dto;
using Events;
using Events.Parsers;
using Game.GameEntity;
using Network.Events;
using UnityEngine;
using EventHandler = Network.Events.Handlers.EventHandler;

namespace Network
{
    public class NetworkPacketManager : MonoBehaviour, PacketListener
    {
        private Network network;

        public EntityManager entityManager;

        public bool connected = false;
        
        public List<Packet> bufferedPackets = new List<Packet>();

        public void Start()
        {
            network = new Network(this);
            TryConnect();

        }

        public void TryConnect()
        {
            if (network.Connect())
            {
                Debug.Log("Connected");
                connected = true;

                SendBufferedPackets();
            }
            else
            {
                Debug.LogWarning("Trying to connect remote server...");
                Invoke(nameof(TryConnect), 1000);
            }
        }

        private void SendBufferedPackets()
        {
            foreach (var packet in bufferedPackets)
            {
                Debug.Log("Sending buffered packet");
                network.Send(packet);
            }
        }


        public void Send(GameEvent gameEvent)
        {
            var gamePacket = new Packet(gameEvent.Serialize());
            if (connected)
            {
                network.Send(gamePacket);
            }
            else
            {
                Debug.LogWarning("Unable to send packet. Not connected to server");
                bufferedPackets.Add(gamePacket);
            }
        }

        public void onReceive(Packet packet)
        {
            
//                Debug.Log("Received packet for player " + playerIdentifier.Id);
                EventHandler eventHandler = EventHandlerFactory.Parse(packet);

                if (entityManager.Exists(eventHandler.GetPlayerIdentifier()))
                {
                    Entity entity = entityManager.GetEntity(eventHandler.GetPlayerIdentifier());
                    //entity.OnAction(eventHandler);
                }
                else
                {
                    Debug.Log("Player " + eventHandler.GetPlayerIdentifier().Id + " does not exist");
                }
            
            
        }
    }
}