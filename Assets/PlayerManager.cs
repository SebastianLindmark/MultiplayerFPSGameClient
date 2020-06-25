using System.Collections;
using System.Collections.Generic;
using dto;
using Events;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Dictionary<int, Player> playerMap = new Dictionary<int, Player>();
    
    public NetworkPacketManager networkPacketManager;
    
    public void Add(PlayerIdentifier playerIdentifier, Player player)
    {
        if (!Exists(playerIdentifier))
        {
            playerMap[playerIdentifier.Id] = player;
            var joinEvent = new JoinEvent(playerIdentifier);
            networkPacketManager.Send(joinEvent);
        }

    }
    
    public bool Exists(PlayerIdentifier playerIdentifier)
    {
        return playerMap.ContainsKey(playerIdentifier.Id);
    }

    public Player GetPlayer(PlayerIdentifier playerIdentifier)
    {
        if (Exists(playerIdentifier))
        {
            return playerMap[playerIdentifier.Id];
        }

        throw new PlayerNotFoundException(playerIdentifier.Id);
    }

    public PlayerIdentifier GetLocalPlayerIdentifier()
    {
        return new PlayerIdentifier(1337);
    }

}
