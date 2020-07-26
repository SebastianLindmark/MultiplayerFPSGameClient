using System;
using System.Collections.Generic;
using System.Linq;
using dto;
using UnityEngine;

public class NetworkEntityManager : MonoBehaviour
{
    private readonly Dictionary<int, NetworkEntity> playerMap = new Dictionary<int, NetworkEntity>();

    private void Start()
    {
        var networkEntities = FindObjectsOfType<MonoBehaviour>().OfType<NetworkEntity>();
        foreach (var entity in networkEntities)
        {   
            Debug.Log("Adding entity with id " + entity.getId());
            Add(entity.getId(), entity);
        }
    }

    public void Add(PlayerIdentifier playerIdentifier, NetworkEntity player)
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

    public NetworkEntity GetPlayer(PlayerIdentifier playerIdentifier)
    {
        if (Exists(playerIdentifier))
        {
            return playerMap[playerIdentifier.Id];
        }

        throw new PlayerNotFoundException(playerIdentifier.Id);
    }
}