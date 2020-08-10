using System.Collections.Generic;
using dto;
using Events;
using Network;
using UnityEngine;

namespace Game.GameEntity
{
    public class EntityManager : MonoBehaviour
    {
        private readonly Dictionary<int, Entity> entityMap = new Dictionary<int, Entity>();
        public NetworkPacketManager networkPacketManager;
    
        public void Add(PlayerIdentifier playerIdentifier, Entity entity)
        {
            if (Exists(playerIdentifier)) return;
            
            entityMap[playerIdentifier.Id] = entity;
            var joinEvent = new JoinEvent(playerIdentifier, entity.Name());
            
            Debug.Log("Sending join event for entity " + entity.Name());
            networkPacketManager.Send(joinEvent);

        }
    
        public bool Exists(PlayerIdentifier playerIdentifier)
        {
            return entityMap.ContainsKey(playerIdentifier.Id);
        }

        public Entity GetEntity(PlayerIdentifier playerIdentifier)
        {
            if (Exists(playerIdentifier))
            {
                return entityMap[playerIdentifier.Id];
            }

            throw new PlayerNotFoundException(playerIdentifier.Id);
        }
    }
}
