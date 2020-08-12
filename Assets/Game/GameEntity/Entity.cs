using System;
using System.Collections.Concurrent;
using dto;
using UnityEngine;
using EventHandler = Network.Events.Handlers.EventHandler;
using Random = UnityEngine.Random;

namespace Game.GameEntity
{
    public class Entity : MonoBehaviour
    {
        

        public PlayerIdentifier playerIdentifier { get; set; }

        private void Start()
        {
            if (playerIdentifier == null)
            {
                playerIdentifier = new PlayerIdentifier(Random.Range(0, 10000));
            }
        }

        public virtual void OnAction(EventHandler eventHandler)
        {
            
        }

        public virtual string Name()
        {
            return playerIdentifier.Id.ToString();
        }
    }
}