using System.Collections.Generic;
using System.Linq;
using Events;
using Network;
using NetworkPlayer;
using UnityEngine;

public class NetworkPacketManager : MonoBehaviour
{

    private Network.Network network;        
    
    public void Start()
    {
        network = new Network.Network();
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


}