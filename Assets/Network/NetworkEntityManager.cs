using System.Collections.Generic;
using System.Linq;
using dto;
using Game.Entity;
using Game.GameEntity;
using UnityEngine;

namespace Network
{
    public class NetworkEntityManager : MonoBehaviour
    {
        private readonly Dictionary<int, Entity> playerMap = new Dictionary<int, Entity>();

        private void Start()
        {
            var networkEntities = FindObjectsOfType<MonoBehaviour>().OfType<Entity>();
            foreach (var entity in networkEntities)
            {   
                Debug.Log("Adding entity with id " + entity.playerIdentifier.Id);
                Add(entity.playerIdentifier, entity);
            }
        }

        public void Add(PlayerIdentifier playerIdentifier, Entity player)
        {
            if (!Exists(playerIdentifier))
            {
                playerMap[playerIdentifier.Id] = player;
            }
        }

        public bool Exists(PlayerIdentifier playerIdentifier)
        {
            return playerMap.ContainsKey(playerIdentifier.Id);
        }

        public Entity GetEntity(PlayerIdentifier playerIdentifier)
        {
            if (Exists(playerIdentifier))
            {
                return playerMap[playerIdentifier.Id];
            }

            throw new PlayerNotFoundException(playerIdentifier.Id);
        }
    }
}