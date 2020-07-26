using System.Collections.Generic;
using dto;
using Events;
using Network;
using UnityEngine;

namespace Game
{
    public class EntityManager : MonoBehaviour
    {
        private readonly Dictionary<int, Player> entityMap = new Dictionary<int, Player>();
        public NetworkPacketManager networkPacketManager;
    
        public void Add(PlayerIdentifier playerIdentifier, Player player)
        {
            if (Exists(playerIdentifier)) return;
            
            entityMap[playerIdentifier.Id] = player;
            var joinEvent = new JoinEvent(playerIdentifier, player.username);
            
            Debug.Log("Sending join event for player " + player.username);
            networkPacketManager.Send(joinEvent);

        }
    
        public bool Exists(PlayerIdentifier playerIdentifier)
        {
            return entityMap.ContainsKey(playerIdentifier.Id);
        }

        public Player GetEntity(PlayerIdentifier playerIdentifier)
        {
            if (Exists(playerIdentifier))
            {
                return entityMap[playerIdentifier.Id];
            }

            throw new PlayerNotFoundException(playerIdentifier.Id);
        }
    }
}
