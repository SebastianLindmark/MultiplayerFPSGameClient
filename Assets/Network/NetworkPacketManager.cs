using System;
using System.Collections.Generic;
using dto;
using Events;
using Events.Parsers;
using Game;
using Game.Entity;
using Game.GameEntity;
using UnityEngine;
using EventHandler = Events.Handlers.EventHandler;

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

            //network.InitiateClient(framedPlayerAttribute.GetPlayerIdentifier());

            //The error is because we are not including the type of the action in the game action serializations.
            //Also, WelcomePacket should be a GameEvent -> Serialize -> Packet instead.
            //Also, should network manager really have a reference to framedPlayerAttribute? Feels like network manager
            //is more general and should instead by used by framedPlayerAttribute. Thanks
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


        //This class is quite simple at the moment. But we will use this class to direct packets to the correct player/AI handler. 


        public void onReceive(Packet packet, PlayerIdentifier playerIdentifier)
        {
            
                Debug.Log("Received packet for player " + playerIdentifier.Id);
                EventHandler eventHandler = EventHandlerFactory.Parse(packet);
                //Maybe have an "entityManager" instead where all object has a method called handlePacket().
                //We will need to pass packets to other than players.
                
                
                if (entityManager.Exists(playerIdentifier))
                {
                    Entity entity = entityManager.GetEntity(playerIdentifier);
                    entity.OnAction(eventHandler);
                }
                else
                {
                    Debug.Log("Player " + eventHandler.GetPlayerIdentifier().Id + " does not exist");
                }
            
            
        }
    }
}