using System.Collections.Concurrent;
using dto;
using Network.Events.Handlers;
using UnityEngine;

namespace Game.GameEntity
{
    public class Player : Entity
    {
    
        protected readonly ConcurrentQueue<EventHandler> eventHandlerQueue = new ConcurrentQueue<EventHandler>();
        
        public int playerId;
        public string username;
       

        void Start()
        {
            playerIdentifier = new PlayerIdentifier(playerId);

            GameObject gameObj = GameObject.FindWithTag("EntityManager");

            if (gameObj != null)
            {
                EntityManager playerManager = gameObj.GetComponent<EntityManager>();
                playerManager.Add(playerIdentifier, this);
            }
        
        }

        public override string Name()
        {
            return username;
        }
        
        void Update()
        {
            if (eventHandlerQueue.TryDequeue(out var eventHandler))
            {
                eventHandler.Execute(this);
            }
        }

        public override void OnAction(EventHandler eventHandler)
        {
            eventHandlerQueue.Enqueue(eventHandler);
        }
        
    }
}