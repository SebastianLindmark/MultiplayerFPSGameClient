using System;
using System.Collections.Generic;
using System.Linq;
using dto;
using Events;
using Events.Parsers;
using Network;
using NetworkPlayer;
using UnityEngine;
using EventHandler = Events.Handlers.EventHandler;

public class NetworkPacketManager : MonoBehaviour, PacketListener
{

    private Network.Network network;

    public PlayerManager playerManager;
    
    public void Start()
    {
        network = new Network.Network(this);
        network.Start();
        
        //network.InitiateClient(framedPlayerAttribute.GetPlayerIdentifier());
        
        //The error is because we are not including the type of the action in the game action serializations.
        //Also, WelcomePacket should be a GameEvent -> Serialize -> Packet instead.
        //Also, should network manager really have a reference to framedPlayerAttribute? Feels like network manager
        //is more general and should instead by used by framedPlayerAttribute. Thanks
        
    }
    

    public void Send(GameEvent gameEvent)
    {
        var gamePacket = new Packet(gameEvent.Serialize());
        network.Send(gamePacket);
    }
    
    
    
    
    //This class is quite simple at the moment. But we will use this class to direct packets to the correct player/AI handler. 


    public void onReceive(Packet packet)
    {
        Debug.Log("Received packet");
        EventHandler eventHandler =  ParserFactory.Parse(packet);
        
        //Maybe have an "entityManager" instead where all object has a method called handlePacket().
        //We will need to pass packets to other than players.
        
        
        
        if (playerManager.Exists(eventHandler.GetPlayerIdentifier()))
        {
            Player player = playerManager.GetPlayer(new PlayerIdentifier(666));
            player.OnAction(eventHandler);    
        }
        else
        {
            Debug.Log("Player " + eventHandler.GetPlayerIdentifier().Id + " does not exist" );
        }



    }
}