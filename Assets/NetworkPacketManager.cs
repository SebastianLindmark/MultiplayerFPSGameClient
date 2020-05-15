using System.Collections.Generic;
using System.Linq;
using Events;
using Network;
using NetworkPlayer;
using UnityEngine;

public class NetworkPacketManager : MonoBehaviour
{

    
    public FramedPlayerAttribute framedPlayerAttribute;
    
    private Network.Network network;        
    
    public void Start()
    {
        network = new Network.Network();
        network.Start();
        network.InitiateClient(framedPlayerAttribute.GetPlayerIdentifier());
        
        
        InvokeRepeating("Send", 1f, 0.5f);
    }


    private void Send()
    {
            
        List<GameEvent> gameEvents = framedPlayerAttribute.GetAttributeFrame();
        Debug.Log("Sending " + gameEvents.Count + " game events to server");
        gameEvents.Select(ge => new GamePacket(ge.Serialize()))
            .ToList().ForEach(packet => network.Send(packet));
    }


}